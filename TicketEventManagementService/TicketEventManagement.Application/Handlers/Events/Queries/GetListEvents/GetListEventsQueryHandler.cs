using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Events.Queries.GetListEvents;

internal class GetListEventsQueryHandler : IRequestHandler<GetListEventsQuery, IReadOnlyCollection<GetEventDto>>
{
    private readonly IBaseRepository<Event> _events;

    private readonly IMapper _mapper;

    public GetListEventsQueryHandler(IBaseRepository<Event> events, IMapper mapper)
    {
        _events = events;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetEventDto>> Handle(GetListEventsQuery request, 
        CancellationToken cancellationToken)
    {

        var events = await _events.GetListAsync(
            request.Offset,
            request.Limit,
            e=>((request.CategoryId == null) || (e.CategoryId == request.CategoryId))
            && ((request.LocationId == null) || (e.LocationId == request.LocationId))
            && ((request.DateEvent == null) || (e.DateTimeEventStart.Date == request.DateEvent.Value.Date))
            && ((request.IsActive == null) || (e.IsActive == request.IsActive)),
            e => e.DateTimeEventStart,
            false,
            cancellationToken);

        return _mapper.Map<IReadOnlyCollection<GetEventDto>>(events);
    }
}
