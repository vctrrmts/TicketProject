using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketControlService.Application.Abstractions.Service;
using TicketControlService.Application.Abstractions.Persistence;
using TicketControlService.Domain;
using TicketControlService.Application.Abstractions.ExternalProviders;
using System.Text.Json;

namespace TicketControlService.Application.Handlers.Commands.VerifyTicket
{
    public class VerifyTicketCommandHandler : IRequestHandler<VerifyTicketCommand, bool>
    {
        private readonly IBaseRepository<Ticket> _tickets;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly ICurrentUserService _currentUserService;

        public VerifyTicketCommandHandler(IBaseRepository<Ticket> tickets, ITicketsRepository ticketsRepository, 
            ICurrentUserService currentUserService) 
        {
            _tickets = tickets;
            _ticketsRepository = ticketsRepository;
            _currentUserService = currentUserService;
        }
        public async Task<bool> Handle(VerifyTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _tickets.SingleOrDefaultAsync(x=>x.HashGuid == request.HashFromQR, cancellationToken);
            if (ticket != null) 
            {
                return false;
            }

            var accessToken = _currentUserService.AccessToken;
            ticket = await _ticketsRepository.GetTicketAsync(request.HashFromQR, cancellationToken);
            if (ticket.EventId != request.EventId)
            {
                return false;
            }

            await _tickets.AddAsync(ticket,cancellationToken);
            return true;
        }
    }
}
