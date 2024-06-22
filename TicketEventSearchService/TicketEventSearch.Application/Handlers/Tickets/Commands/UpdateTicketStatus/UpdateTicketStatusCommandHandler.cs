using MediatR;
using TicketEventSearch.Domain;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Ticket;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain.Enums;
using AutoMapper;
using System.Text.Json;
using Serilog;
using System.Linq.Expressions;

namespace TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus;

public class UpdateTicketStatusCommandHandler : 
    IRequestHandler<UpdateTicketStatusCommand, IReadOnlyCollection<GetTicketForSentMailDto>>
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

    public async Task<IReadOnlyCollection<GetTicketForSentMailDto>> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
    {
        var tickets = await _tickets.GetListAsync(default, default, x => request.TicketIds.Contains(x.TicketId),
            default,default,cancellationToken);

        foreach (var ticketId in request.TicketIds)
        {
            if (tickets.SingleOrDefault(x=>x.TicketId == ticketId) is null)
            {
                throw new NotFoundException($"Ticket with TicketId = {ticketId} not found");
            }

            if (tickets.Single(x => x.TicketId == ticketId).TicketStatusId == (int)TicketStatusEnum.Ordered)
            {
                throw new BadOperationException($"Ticket with TicketId = {ticketId} already ordered");
            }
        }

        foreach (var ticket in tickets)
        {
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
        }


        _cleanTicketCacheService.ClearAllTicketCaches();

        return _mapper.Map<IReadOnlyCollection<GetTicketForSentMailDto>>(tickets);
    }
}
