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
using TicketEventSearch.Application.Handlers.Seats.Queries.GetListSeats;
using TicketEventSearch.Application.Abstractions.Caches.Seat;

namespace SearchService.UnitTests.Tests.Seats.Queries.GetListSeats;

public class GetListSeatsQueryHandlerTest : RequestHandlerTestBase<GetListSeatsQuery, IReadOnlyCollection<GetSeatDto>>
{
    private readonly Mock<IBaseRepository<Seat>> _seatsMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ISeatListCache> _cacheMock = new();

    public GetListSeatsQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetSeatDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetListSeatsQuery, IReadOnlyCollection<GetSeatDto>> CommandHandler 
        => new GetListSeatsQueryHandler(_seatsMock.Object, _mapper, _cacheMock.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(GetListSeatsQuery query)
    {
        // arrange
        var seats = new Seat[10];
        for (int i = 0; i < 10; i++)
        {
            seats[i] = new Seat(Guid.NewGuid(), Guid.NewGuid(), "Sector", 1,1);
        }

        _seatsMock.Setup(
            p => p.GetListAsync(query.Offset, query.Limit, It.IsAny<Expression<Func<Seat, bool>>>(),
            default, default, default)).ReturnsAsync(seats);

        // act and assert
        await AssertNotThrow(query);
    }
}
