using AutoMapper;
using TicketEventSearch.Application.Abstractions.Caches.Event;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Events.Queries.GetEventById;

public class GetEventByIdQueryHandler : BaseCashedQuery<GetEventByIdQuery, GetEventDto>
{
    private readonly IBaseRepository<Event> _events;

    private readonly IMapper _mapper;

    public GetEventByIdQueryHandler(IBaseRepository<Event> events, IMapper mapper, IEventCache cache) : base(cache)
    {
        _events = events;
        _mapper = mapper;
    }

    public override async Task<GetEventDto> SentQueryAsync(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        Event? eventt = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId, cancellationToken);

        if (eventt == null)
        {
            throw new NotFoundException($"Event with id = {request.EventId} not found");
        }

        return _mapper.Map<GetEventDto>(eventt);
    }
}
