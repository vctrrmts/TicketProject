using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Location;
using TicketEventSearch.Application.Caches.Seat;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Seats.Commands.AddRangeSeats;

public class AddRangeSeatsCommandHandler : IRequestHandler<AddRangeSeatsCommand, IReadOnlyCollection<GetSeatDto>>
{
    private readonly IBaseRepository<Seat> _seats;

    private readonly IMapper _mapper;

    private readonly ICleanSeatCacheService _cleanSeatCacheService;

    public AddRangeSeatsCommandHandler(IBaseRepository<Seat> seats, 
        IMapper mapper, ICleanSeatCacheService cleanSeatCacheService)
    {
        _seats = seats;
        _mapper = mapper;
        _cleanSeatCacheService = cleanSeatCacheService;
    }

    public async Task<IReadOnlyCollection<GetSeatDto>> Handle(AddRangeSeatsCommand request, CancellationToken cancellationToken)
    {
        Seat[] newSeats = new Seat[request.Seats.Length];

        for (int i = 0; i < request.Seats.Length; i++)
        {
            newSeats[i] = new Seat(request.Seats[i].SeatId, request.Seats[i].SchemeId,
                request.Seats[i].Sector, request.Seats[i].Row, request.Seats[i].SeatNumber);
        }

        newSeats = await _seats.AddRangeAsync(newSeats.ToArray(), cancellationToken);
        Log.Information("Range of seats added " + JsonSerializer.Serialize(request));

        _cleanSeatCacheService.ClearAllSeatCaches();

        return _mapper.Map<IReadOnlyCollection<GetSeatDto>>(newSeats);
    }
}
