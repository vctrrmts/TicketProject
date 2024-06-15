using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Events.Commands.CreateEvent;

internal class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, GetEventDto>
{
    private readonly IBaseRepository<Event> _events;
    private readonly IBaseRepository<Location> _locations;

    private readonly IMapper _mapper;


    public CreateEventCommandHandler(IBaseRepository<Event> events, 
        IBaseRepository<Location> locations,IMapper mapper)
    {
        _events = events;
        _locations = locations;
        _mapper = mapper;
    }

    public async Task<GetEventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var location = _locations.SingleOrDefaultAsync(e => e.LocationId == request.LocationId, cancellationToken);
        if (location is null)
        {
            throw new NotFoundException($"Location with LocationId = {request.LocationId} not found");
        }

        var newEvent = new Event(request.LocationId, request.CategoryId, request.Name, request.Description, 
            request.UriMainImage, request.DateTimeEventStart, request.DateTimeEventEnd);
        newEvent = await _events.AddAsync(newEvent, cancellationToken);
        Log.Information("Event added " + JsonSerializer.Serialize(request));

        return _mapper.Map<GetEventDto>(newEvent);
    }
}
