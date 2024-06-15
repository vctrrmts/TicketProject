using MediatR;
using Serilog;
using System.Text.Json;
using TicketBuying.Application.Abstractions.Persistence;
using TicketBuying.Application.Exceptions;
using TicketBuying.Domain;

namespace TicketBuying.Application.Handlers.Commands.VerifyTicketCommand
{
    public class VerifyTicketCommandHandler : IRequestHandler<VerifyTicketCommand, BuyedTicket>
    {
        private readonly IBaseRepository<BuyedTicket> _tickets;

        public VerifyTicketCommandHandler(IBaseRepository<BuyedTicket> tickets)
        {
            _tickets = tickets;
        }

        public async Task<BuyedTicket> Handle(VerifyTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _tickets.SingleOrDefaultAsync(x => x.HashGuid == request.HashGuid, cancellationToken);
            if (ticket is null)
            {
                throw new NotFoundException($"Ticket with that HashGuid not found");
            }
            return ticket;
        }
    }
}
