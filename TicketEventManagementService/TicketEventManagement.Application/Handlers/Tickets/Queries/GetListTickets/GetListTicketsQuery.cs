using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Tickets.Queries.GetListTickets;

public class GetListTicketsQuery : IRequest<IReadOnlyCollection<GetTicketDto>>
{
    public Guid? EventId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}
