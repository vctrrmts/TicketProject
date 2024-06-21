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
using TicketEventSearch.Application.Abstractions.Caches.Event;
using TicketEventSearch.Application.Handlers.Events.Queries.GetEvents;

namespace SearchService.UnitTests.Tests.Events.Queries.GetEvents;

public class GetEventsQueryHandlerTest : RequestHandlerTestBase<GetEventsQuery, IReadOnlyCollection<GetEventDto>>
{
    private readonly Mock<IBaseRepository<Event>> _eventsMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<IEventListCache> _cacheMock = new();

    public GetEventsQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetEventDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetEventsQuery, IReadOnlyCollection<GetEventDto>> CommandHandler 
        => new GetEventsQueryHandler(_eventsMock.Object, _mapper, _cacheMock.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(GetEventsQuery query)
    {
        // arrange
        var events = new Event[10];
        for (int i = 0; i < 10; i++)
        {
            events[i] =  new Event(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Name", "Description",
            "UriMainImage.com", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(2), true);
        }

        _eventsMock.Setup(
            p => p.GetListAsync(query.Offset, query.Limit, It.IsAny<Expression<Func<Event, bool>>>(),
            default, default, default)).ReturnsAsync(events);

        // act and assert
        await AssertNotThrow(query);
    }
}
