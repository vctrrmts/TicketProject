using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Event;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, GetEventDto>
{
    private readonly IBaseRepository<Event> _events;

    private readonly IMapper _mapper;

    private readonly ICleanEventCacheService _cleanEventCacheService;

    public UpdateEventCommandHandler(IBaseRepository<Event> events, IMapper mapper,
        ICleanEventCacheService cleanEventCacheService)
    {
        _events = events;
        _mapper = mapper;
        _cleanEventCacheService = cleanEventCacheService;
    }
    public async Task<GetEventDto> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        Event? eventForUpdate = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId, cancellationToken);
        if (eventForUpdate == null)
        {
            throw new NotFoundException($"Event with EventId = {request.EventId} not found");
        }

        eventForUpdate.UpdateName(request.Name);
        eventForUpdate.UpdateDescription(request.Description);
        eventForUpdate.UpdateUriMainImage(request.UriMainImage);
        eventForUpdate.UpdateDateTimeEventStart(request.DateTimeEventStart);
        eventForUpdate.UpdateDateTimeEventEnd(request.DateTimeEventEnd);
        eventForUpdate.UpdateIsActive(request.IsActive);

        await _events.UpdateAsync(eventForUpdate, cancellationToken);
        Log.Information("Event updated " + JsonSerializer.Serialize(request));

        _cleanEventCacheService.ClearAllEventCaches();

        return _mapper.Map<GetEventDto>(eventForUpdate);
    }
}
