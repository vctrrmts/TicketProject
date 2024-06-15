using MediatR;
using TicketBuying.Domain;

namespace TicketBuying.Application.Handlers.Queries.GetTicketsByEventId;

public class GetTicketsByEventIdQuery : IRequest<IReadOnlyCollection<BuyedTicket>>
{
    public Guid EventId { get; set; }
}
