using Core.Tests;
using System.Linq.Expressions;
using Moq;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using Xunit.Abstractions;
using MediatR;
using TicketEventSearch.Application.Caches.Seat;
using TicketEventSearch.Application.Handlers.Seats.Commands.DeleteRangeSeats;

namespace SearchService.UnitTests.Tests.Seats.Commands.DeleteRangeSeats;

public class DeleteRangeSeatsCommandHandlerTest : RequestHandlerTestBase<DeleteRangeSeatsCommand>
{
    private readonly Mock<IBaseRepository<Seat>> _seatsMock = new();
    private readonly Mock<ICleanSeatCacheService> _cleanSeatCacheService = new();

    public DeleteRangeSeatsCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IRequestHandler<DeleteRangeSeatsCommand> CommandHandler 
        => new DeleteRangeSeatsCommandHandler(_seatsMock.Object, _cleanSeatCacheService.Object);

    [Fact]
    public async Task Should_BeValid()
    {
        // arrange
        var seatIds = new Guid[5] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        var command = new DeleteRangeSeatsCommand() { Seats = seatIds };


        var seats = new Seat[5];
        for (int i = 0; i < 5; i++)
        {
            seats[i] = new Seat(seatIds[i], Guid.NewGuid(), "Sector", 1, 1);
        }

        _seatsMock.Setup(p => p.GetListAsync(default, default, It.IsAny<Expression<Func<Seat, bool>>>(), 
            default, default, default)).ReturnsAsync(seats);

        _seatsMock.Setup(p => p.DeleteRangeAsync(seats, default));

        _cleanSeatCacheService.Object.ClearAllSeatCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
