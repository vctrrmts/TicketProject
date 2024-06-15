using MediatR;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketsByEventId;

public class GetTicketsByEventIdQuery : IRequest<IReadOnlyCollection<GetTicketDto>>
{
    public Guid EventId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}
