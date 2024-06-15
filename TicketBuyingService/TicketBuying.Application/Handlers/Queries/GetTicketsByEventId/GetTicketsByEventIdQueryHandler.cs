using MediatR;
using TicketBuying.Application.Abstractions.Persistence;
using TicketBuying.Application.Exceptions;
using TicketBuying.Domain;

namespace TicketBuying.Application.Handlers.Queries.GetTicketsByEventId
{
    public class GetTicketsByEventIdQueryHandler : IRequestHandler<GetTicketsByEventIdQuery, IReadOnlyCollection<BuyedTicket>>
    {
        private readonly IBaseRepository<BuyedTicket> _tickets;

        public GetTicketsByEventIdQueryHandler(IBaseRepository<BuyedTicket> tickets)
        {
            _tickets = tickets;
        }
        public async Task<IReadOnlyCollection<BuyedTicket>> Handle(GetTicketsByEventIdQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _tickets.GetListAsync(
                default,
                default,
                e => e.EventId == request.EventId,
                null,
                false,
                cancellationToken);

            return tickets;
        }
    }
}
