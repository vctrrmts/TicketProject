using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Events.Commands.UpdateEvent;

internal class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, GetEventDto>
{
    private readonly IBaseRepository<Event> _events;
    private readonly IEventsRepository _eventsRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public UpdateEventCommandHandler(IBaseRepository<Event> events, IMapper mapper,
        IEventsRepository eventsRepository, ICurrentUserService currentUserService)
    {
        _events = events;
        _eventsRepository = eventsRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
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
        if (eventForUpdate.IsPublished)
        {
            string token = _currentUserService.AccessToken;
            await _eventsRepository.UpdateEventAsync(_mapper.Map<EventForExportDto>(eventForUpdate),
                token, cancellationToken);
        }
        Log.Information("Event updated " + JsonSerializer.Serialize(request));

        return _mapper.Map<GetEventDto>(eventForUpdate);
    }
}
