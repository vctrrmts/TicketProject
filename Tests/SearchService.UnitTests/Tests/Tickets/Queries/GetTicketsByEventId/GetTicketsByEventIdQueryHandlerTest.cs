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
using AutoFixture;
using TicketEventSearch.Application.Abstractions.Caches.Ticket;
using TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketsByEventId;

namespace SearchService.UnitTests.Tests.Tickets.Queries.GetTicketsByEventId;

public class GetTicketsByEventIdQueryHandlerTest : RequestHandlerTestBase<GetTicketsByEventIdQuery, IReadOnlyCollection<GetTicketDto>>
{
    private readonly Mock<IBaseRepository<Ticket>> _ticketsMock = new();
    private readonly Mock<IBaseRepository<Event>> _eventsMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ITicketListCache> _cacheMock = new();

    public GetTicketsByEventIdQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetTicketDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetTicketsByEventIdQuery, IReadOnlyCollection<GetTicketDto>> CommandHandler 
        => new GetTicketsByEventIdQueryHandler(_ticketsMock.Object, _eventsMock.Object, _mapper, _cacheMock.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_EventFound(GetTicketsByEventIdQuery query)
    {
        // arrange
        var myEvent = new Event(query.EventId, Guid.NewGuid(), Guid.NewGuid(), "Name", "Description",
            "UriMainImage.com", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(2), true);

        _eventsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Event, bool>>>(), default)
        ).ReturnsAsync(myEvent);

        var tickets = TestFixture.Build<Ticket>().CreateMany(10).ToArray();

        _ticketsMock.Setup(
            p => p.GetListAsync(query.Offset, query.Limit, It.IsAny<Expression<Func<Ticket, bool>>>(),
            default, default, default)).ReturnsAsync(tickets);

        // act and assert
        await AssertNotThrow(query);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_EventNotFound(GetTicketsByEventIdQuery query)
    {
        // arrange
        _eventsMock.Setup(p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Event, bool>>>(), default)
        ).ReturnsAsync(null as Event);

        // act and assert
        await AssertThrowNotFound(query);
    }
}
