using AutoMapper;
using TicketEventSearch.Application.Abstractions.Caches.Seat;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Seats.Queries.GetListSeats;

internal class GetListSeatsQueryHandler : BaseCashedQuery<GetListSeatsQuery, IReadOnlyCollection<GetSeatDto>>
{
    private readonly IBaseRepository<Seat> _seats;

    private readonly IMapper _mapper;

    public GetListSeatsQueryHandler(IBaseRepository<Seat> seats, IMapper mapper, ISeatListCache cache) : base(cache)
    {
        _seats = seats;
        _mapper = mapper;
    }

    public override async Task<IReadOnlyCollection<GetSeatDto>> SentQueryAsync(GetListSeatsQuery request, CancellationToken cancellationToken)
    {
        var seats = await _seats.GetListAsync(
            request.Offset,
            request.Limit,
            e => e.SchemeId == request.SchemeId,
            e => e.Sector,
            false,
            cancellationToken);

        seats = seats.OrderBy(e => e.Sector).ThenBy(e => e.Row).ThenBy(e => e.SeatNumber).ToArray();

        return _mapper.Map<IReadOnlyCollection<GetSeatDto>>(seats);
    }
}
