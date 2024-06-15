using AutoMapper;
using TicketEventSearch.Application.Abstractions.Caches.Event;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Events.Queries.GetEvents;

public class GetEventsQueryHandler : BaseCashedQuery<GetEventsQuery, IReadOnlyCollection<GetEventDto>>
{
    private readonly IBaseRepository<Event> _events;
    private readonly IMapper _mapper;

    public GetEventsQueryHandler(IBaseRepository<Event> events, IMapper mapper, IEventListCache cache) : base(cache)
    {
        _events = events;
        _mapper = mapper;
    }

    public override async Task<IReadOnlyCollection<GetEventDto>> SentQueryAsync(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _events.GetListAsync(
            request.Offset,
            request.Limit,
            e => ((request.CategoryId == null) || (e.CategoryId == request.CategoryId))
            && ((request.LocationId == null) || (e.LocationId == request.LocationId))
            && ((request.DateEvent == null) || (e.DateTimeEventStart.Date == request.DateEvent.Value.Date))
            && e.IsActive == true,
            e => e.DateTimeEventStart,
            false,
            cancellationToken);

        return _mapper.Map<IReadOnlyCollection<GetEventDto>>(events);
    }
}
