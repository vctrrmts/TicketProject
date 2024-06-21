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
using TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketById;
using TicketEventSearch.Application.Abstractions.Caches.Ticket;

namespace SearchService.UnitTests.Tests.Tickets.Queries.GetTicketById;

public class GetTicketByIdQueryHandlerTest : RequestHandlerTestBase<GetTicketByIdQuery, GetTicketDto>
{
    private readonly Mock<IBaseRepository<Ticket>> _ticketsMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ITicketCache> _cacheMock = new();

    public GetTicketByIdQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetTicketDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetTicketByIdQuery, GetTicketDto> CommandHandler 
        => new GetTicketByIdQueryHandler(_ticketsMock.Object, _mapper, _cacheMock.Object);

    [Fact]
    public async Task Should_BeValid_When_TicketFound()
    {
        // arrange
        var ticket = TestFixture.Build<Ticket>().Create();
        var query = new GetTicketByIdQuery() { TicketId = ticket.TicketId };

        _ticketsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Ticket, bool>>>(), default)
        ).ReturnsAsync(ticket);

        // act and assert
        await AssertNotThrow(query);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_TicketNotFound(GetTicketByIdQuery query)
    {
        // arrange
        _ticketsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Ticket, bool>>>(), default)
        ).ReturnsAsync(null as Ticket);

        // act and assert
        await AssertThrowNotFound(query);
    }
}
