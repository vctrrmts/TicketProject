using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Seat;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Seats.Commands.DeleteRangeSeats;

public class DeleteRangeSeatsCommandHandler : IRequestHandler<DeleteRangeSeatsCommand>
{
    private readonly IBaseRepository<Seat> _seats;

    private readonly ICleanSeatCacheService _cleanSeatCacheService;

    public DeleteRangeSeatsCommandHandler(IBaseRepository<Seat> seats, ICleanSeatCacheService cleanSeatCacheService)
    {
        _seats = seats;
        _cleanSeatCacheService = cleanSeatCacheService;
    }

    public async Task Handle(DeleteRangeSeatsCommand request, CancellationToken cancellationToken)
    {
        var seatsForDelete = await _seats.GetListAsync(
            null,
            null,
            e => request.Seats.Contains(e.SeatId),
            null,
            false,
            cancellationToken);

        await _seats.DeleteRangeAsync(seatsForDelete, cancellationToken);
        Log.Information("Range of seats deleted " + JsonSerializer.Serialize(request));

        _cleanSeatCacheService.ClearAllSeatCaches();
    }
}
