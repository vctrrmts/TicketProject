using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Tickets.Commands.DeleteTicket;

internal class DeleteTicketsCommandHandler : IRequestHandler<DeleteTicketsCommand>
{
    private readonly IBaseRepository<Ticket> _tickets;
    private readonly IBaseRepository<Event> _events;

    public DeleteTicketsCommandHandler(IBaseRepository<Ticket> tickets, IBaseRepository<Event> events)
    {
        _tickets = tickets;
        _events = events;
    }

    public async Task Handle(DeleteTicketsCommand request, CancellationToken cancellationToken)
    {
        Event? eventOwner = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId);
        if (eventOwner is null)
        {
            throw new NotFoundException($"Event with EventId = {request.EventId} not found");
        }

        var tickets = await _tickets.GetListAsync(
            null,
            null,
            e => e.EventId == request.EventId,
            null,
            false,
            cancellationToken);

        await _tickets.DeleteRangeAsync(tickets);
        Log.Information("Range of tickets deleted " + JsonSerializer.Serialize(request));
    }
}
