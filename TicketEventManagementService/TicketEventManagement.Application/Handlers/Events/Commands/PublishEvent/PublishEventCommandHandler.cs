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

namespace TicketEventManagement.Application.Handlers.Events.Commands.PublishEvent;

internal class PublishEventCommandHandler : IRequestHandler<PublishEventCommand>
{
    private readonly IBaseRepository<Event> _events;

    private readonly IEventsRepository _eventsRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public PublishEventCommandHandler(IBaseRepository<Event> events, 
        IEventsRepository eventsRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _events = events;
        _eventsRepository = eventsRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    public async Task Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        Event? eventById = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId, cancellationToken);
        if (eventById == null)
        {
            throw new NotFoundException($"Event with EventId = {request.EventId} not found");
        }

        if (eventById.IsPublished)
        {
            throw new BadOperationException($"Event with EventId = {request.EventId} already published");
        }

        if (eventById.Tickets.Any(x=>x.Price is null))
        {
            throw new BadOperationException($"Not all tickets have price where EventId = {request.EventId}");
        }

        eventById.UpdateIsPublished();
        await _events.UpdateAsync(eventById, cancellationToken);

        string token = _currentUserService.AccessToken;
        await _eventsRepository.PublishEventAsync(_mapper.Map<EventForExportDto>(eventById), token, cancellationToken);
        Log.Information("Event published " + JsonSerializer.Serialize(request));
    }
}
