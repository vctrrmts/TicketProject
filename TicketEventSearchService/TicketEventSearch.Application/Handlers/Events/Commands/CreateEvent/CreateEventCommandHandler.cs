using MediatR;
using TicketEventSearch.Domain;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Event;
using TicketEventSearch.Application.Caches.Ticket;
using Serilog;
using System.Text.Json;

namespace TicketEventSearch.Application.Handlers.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, bool>
{
    private readonly IBaseRepository<Event> _events;
    private readonly IBaseRepository<Ticket> _tickets;

    private readonly ICleanEventCacheService _cleanEventCacheService;

    public CreateEventCommandHandler(IBaseRepository<Event> events, IBaseRepository<Ticket> tickets,
        ICleanEventCacheService cleanEventCacheService)
    {
        _events = events;
        _tickets = tickets;
        _cleanEventCacheService = cleanEventCacheService;
    }

    public async Task<bool> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Event(
            request.EventId, 
            request.LocationId,
            request.CategoryId,
            request.Name, 
            request.Description, 
            request.UriMainImage,
            request.DateTimeEventStart, 
            request.DateTimeEventEnd,
            request.IsActive);
        await _events.AddAsync(newEvent, cancellationToken);

        var ticketArray = request.Tickets.ToArray();
        Ticket[] tickets = new Ticket[ticketArray.Length];
        for (int i = 0; i < ticketArray.Length; i++)
        {
            tickets[i] = new Ticket(ticketArray[i].TicketId, ticketArray[i].EventId,
                ticketArray[i].SeatId, ticketArray[i].Price);
        }
        await _tickets.AddRangeAsync(tickets, cancellationToken);
        Log.Information("Event with tickets added " + newEvent.EventId);

        _cleanEventCacheService.ClearListEventCaches();
        return true;
    }
}
