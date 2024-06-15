using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Seats.Queries.GetListSeats;

public class GetListSeatsQuery : IRequest<IReadOnlyCollection<GetSeatDto>>
{
    public Guid? SchemeId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}
