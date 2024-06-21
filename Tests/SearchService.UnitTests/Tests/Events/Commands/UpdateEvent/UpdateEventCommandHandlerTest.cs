using Core.Tests;
using System.Linq.Expressions;
using Moq;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using AutoMapper;
using Core.Tests.Fixtures;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using TicketEventSearch.Application.Handlers.Events.Commands.UpdateEvent;
using TicketEventSearch.Application.Caches.Event;

namespace SearchService.UnitTests.Tests.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandlerTest : RequestHandlerTestBase<UpdateEventCommand, GetEventDto>
{
    private readonly Mock<IBaseRepository<Event>> _eventsMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICleanEventCacheService> _cleanEventCacheService = new();

    public UpdateEventCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetEventDto).Assembly).Mapper;
    }

    protected override IRequestHandler<UpdateEventCommand, GetEventDto> CommandHandler 
        => new UpdateEventCommandHandler(_eventsMock.Object, _mapper, _cleanEventCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_EventNotFound(UpdateEventCommand command)
    {
        // arrange
        _eventsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Event, bool>>>(), default)
        ).ReturnsAsync(null as Event);

        // act and assert
        await AssertThrowNotFound(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_EventFound(UpdateEventCommand command)
    {
        // arrange
        command.UriMainImage = "string.com";
        command.DateTimeEventStart = DateTime.Now.AddDays(1);
        command.DateTimeEventEnd = DateTime.Now.AddDays(1).AddHours(2);
        var myEvent = new Event(command.EventId, Guid.NewGuid(), Guid.NewGuid(), command.Name, command.Description, 
            command.UriMainImage, command.DateTimeEventStart, command.DateTimeEventEnd, command.IsActive);

        _eventsMock.Setup(p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Event, bool>>>(), default)
            ).ReturnsAsync(myEvent);

        myEvent.UpdateName(command.Name);
        myEvent.UpdateDescription(command.Description);
        myEvent.UpdateUriMainImage(command.UriMainImage);
        myEvent.UpdateDateTimeEventStart(command.DateTimeEventStart);
        myEvent.UpdateDateTimeEventEnd(command.DateTimeEventEnd);
        myEvent.UpdateIsActive(command.IsActive);

        _eventsMock.Setup(p => p.UpdateAsync(myEvent, default)).ReturnsAsync(myEvent);

        _cleanEventCacheService.Object.ClearAllEventCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
