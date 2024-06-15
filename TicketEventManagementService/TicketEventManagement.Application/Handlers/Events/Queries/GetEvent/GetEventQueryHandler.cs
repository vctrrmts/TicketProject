using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Events.Queries.GetEvent;

internal class GetEventQueryHandler : IRequestHandler<GetEventQuery, GetEventDto>
{
    private readonly IBaseRepository<Event> _events;

    private readonly IMapper _mapper;

    public GetEventQueryHandler(IBaseRepository<Event> events, IMapper mapper)
    {
        _events = events;
        _mapper = mapper;
    }

    public async Task<GetEventDto> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        Event? myEvent = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId);
        if (myEvent is null)
        {
            throw new NotFoundException($"Event with EventId = {request.EventId} not found");
        }

        return _mapper.Map<GetEventDto>(myEvent);
    }
}
