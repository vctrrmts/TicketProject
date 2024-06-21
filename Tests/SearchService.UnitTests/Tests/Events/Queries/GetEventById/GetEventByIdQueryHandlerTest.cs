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
using TicketEventSearch.Application.Handlers.Events.Queries.GetEventById;
using TicketEventSearch.Application.Abstractions.Caches.Event;

namespace SearchService.UnitTests.Tests.Events.Queries.GetEventById;

public class GetEventByIdQueryHandlerTest : RequestHandlerTestBase<GetEventByIdQuery, GetEventDto>
{
    private readonly Mock<IBaseRepository<Event>> _eventsMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<IEventCache> _cacheMock = new();

    public GetEventByIdQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetEventDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetEventByIdQuery, GetEventDto> CommandHandler 
        => new GetEventByIdQueryHandler(_eventsMock.Object, _mapper, _cacheMock.Object);

    [Fact]
    public async Task Should_BeValid_When_EventFound()
    {
        // arrange
        var myEvent = new Event(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Name", "Description",
            "UriMainImage.com", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(2), true);

        var query = new GetEventByIdQuery() { EventId = myEvent.EventId };
        _eventsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Event, bool>>>(), default)
        ).ReturnsAsync(myEvent);

        // act and assert
        await AssertNotThrow(query);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_CategoryNotFound(GetEventByIdQuery query)
    {
        // arrange
        _eventsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Event, bool>>>(), default)
        ).ReturnsAsync(null as Event);

        // act and assert
        await AssertThrowNotFound(query);
    }
}
