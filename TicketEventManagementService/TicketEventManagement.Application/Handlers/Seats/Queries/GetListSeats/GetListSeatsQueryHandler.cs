using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Seats.Queries.GetListSeats;

internal class GetListSeatsQueryHandler : IRequestHandler<GetListSeatsQuery, IReadOnlyCollection<GetSeatDto>>
{
    private readonly IBaseRepository<Seat> _seats;

    private readonly IMapper _mapper;

    public GetListSeatsQueryHandler(IBaseRepository<Seat> seats, IMapper mapper)
    {
        _seats = seats;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetSeatDto>> Handle(GetListSeatsQuery request, CancellationToken cancellationToken)
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
