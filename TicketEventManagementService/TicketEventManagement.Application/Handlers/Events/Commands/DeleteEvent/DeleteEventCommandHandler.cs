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

namespace TicketEventManagement.Application.Handlers.Events.Commands.DeleteEvent;

internal class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IBaseRepository<Event> _events;
    private readonly IEventsRepository _eventsRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public DeleteEventCommandHandler(IBaseRepository<Event> events, 
        IEventsRepository eventsRepository,IMapper mapper, ICurrentUserService currentUserService)
    {
        _events = events;
        _eventsRepository = eventsRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        Event? eventById = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId, cancellationToken);
        if (eventById == null)
        {
            throw new NotFoundException($"Event with EventId = {request.EventId} not found");
        }

        eventById.UpdateIsActive(false);
        await _events.UpdateAsync(eventById, cancellationToken);
        if (eventById.IsPublished)
        {
            string token = _currentUserService.AccessToken;
            await _eventsRepository.UpdateEventAsync(_mapper.Map<EventForExportDto>(eventById), token, cancellationToken);
        }
        Log.Information("Event deleted " + JsonSerializer.Serialize(request));
    }
}
