using MediatR;
using TicketEventSearch.Domain;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Ticket;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain.Enums;
using AutoMapper;
using System.Text.Json;
using Serilog;

namespace TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus
{
    public class UpdateTicketStatusCommandHandler : IRequestHandler<UpdateTicketStatusCommand, GetTicketForSentMailDto>
    {
        private readonly IBaseRepository<Ticket> _tickets;

        private readonly ICleanTicketCacheService _cleanTicketCacheService;

        private readonly IMapper _mapper;

        public UpdateTicketStatusCommandHandler(IBaseRepository<Ticket> tickets,
            ICleanTicketCacheService cleanTicketCacheService, IMapper mapper)
        {
            _tickets = tickets;
            _cleanTicketCacheService = cleanTicketCacheService;
            _mapper = mapper;
        }

        public async Task<GetTicketForSentMailDto> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
        {
            Ticket? ticket = await _tickets.SingleOrDefaultAsync(x => x.TicketId == request.TicketId,
                cancellationToken);
            if (ticket == null)
            {
                throw new NotFoundException($"Ticket with TicketId = {request.TicketId} not found");
            }

            if (ticket.TicketStatusId == (int)TicketStatusEnum.Ordered)
            {
                throw new BadOperationException($"Ticket with TicketId = {request.TicketId} already ordered");
            }

            ticket.UpdateStatusId(request.TicketStatusId);

            if (request.TicketStatusId == (int)TicketStatusEnum.Unavailable)
            {
                ticket.UpdateUnavailableStatusEnd(DateTime.UtcNow.AddMinutes(10));
            }
            else
            {
                ticket.UpdateUnavailableStatusEnd(null);
            }

            await _tickets.UpdateAsync(ticket, cancellationToken);
            Log.Information("Ticket status updated " + JsonSerializer.Serialize(request));

            _cleanTicketCacheService.ClearAllTicketCaches();

            return _mapper.Map<GetTicketForSentMailDto>(ticket);
        }
    }
}
