using MediatR;
using System.Text.Json;
using TicketBuying.Application.Abstractions.ExternalProviders;
using TicketBuying.Application.Abstractions.Persistence;
using TicketBuying.Application.Abstractions.Service;
using TicketBuying.Application.Exceptions;
using TicketBuying.Domain;
using TicketBuying.Application.Utils;
using Serilog;

namespace TicketBuying.Application.Handlers.Commands.BuyTicket
{
    public class BuyTicketCommandHandler : IRequestHandler<BuyTicketCommand>
    {
        private readonly IBaseRepository<BuyedTicket> _tickets;
        private readonly ITicketsRepository _ticketsRepository;

        private readonly IMqService _mqService;

        public BuyTicketCommandHandler(IBaseRepository<BuyedTicket> tickets, ITicketsRepository 
            ticketsProvider, IMqService mqService) 
        {
            _tickets = tickets;
            _ticketsRepository = ticketsProvider;
            _mqService = mqService;
        }

        public async Task Handle(BuyTicketCommand request, CancellationToken cancellationToken)
        {
            if (!BuyedTicket.IsValidEmail(request.Mail))
            {
                throw new BadOperationException($"Incorrect Mail Address {request.Mail}");
            }

            var tickets = await _ticketsRepository.UpdateTicketsStatusAsync(request.TicketIds, cancellationToken);

            foreach (var ticket in tickets)
            {
                var ticketIdHash = HashUtil.GetHashGuid(ticket.TicketId.ToString());

                var buyedTicket = new BuyedTicket(ticket.TicketId, ticket.EventId, ticket.Price,
                    request.Mail, ticketIdHash);

                await _tickets.AddAsync(buyedTicket, cancellationToken);
                Log.Information("Ticket buyed " + JsonSerializer.Serialize(request));
                ticket.Mail = request.Mail;
                ticket.HashGuid = ticketIdHash;
            }
            
            _mqService.SendMessageToExchange("PurchasedTicketExchange", JsonSerializer.Serialize(tickets));

        }
    }
}
