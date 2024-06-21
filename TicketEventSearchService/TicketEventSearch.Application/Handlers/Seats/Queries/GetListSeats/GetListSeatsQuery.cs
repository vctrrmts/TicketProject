using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Seats.Queries.GetListSeats;

public class GetListSeatsQuery : IRequest<IReadOnlyCollection<GetSeatDto>>
{
    public Guid SchemeId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}
