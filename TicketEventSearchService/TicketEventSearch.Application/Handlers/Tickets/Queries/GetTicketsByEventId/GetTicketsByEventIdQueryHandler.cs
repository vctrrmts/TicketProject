using AutoMapper;
using TicketEventSearch.Application.Abstractions.Caches.Ticket;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketsByEventId;

public class GetTicketsByEventIdQueryHandler : BaseCashedQuery<GetTicketsByEventIdQuery, IReadOnlyCollection<GetTicketDto>>
{
    private readonly IBaseRepository<Ticket> _tickets;
    private readonly IBaseRepository<Event> _events;

    private readonly IMapper _mapper;

    public GetTicketsByEventIdQueryHandler(IBaseRepository<Ticket> tickets, IBaseRepository<Event> events,
        IMapper mapper, ITicketListCache cache) : base(cache)
    {
        _events = events;
        _tickets = tickets;
        _mapper = mapper;
    }

    public override async Task<IReadOnlyCollection<GetTicketDto>> SentQueryAsync(GetTicketsByEventIdQuery request, CancellationToken cancellationToken)
    {
        Event? thisEvent = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId, cancellationToken);
        if (thisEvent == null)
        {
            throw new NotFoundException($"Event with id = {request.EventId} not found");
        }

        var tickets = await _tickets.GetListAsync(
            request.Offset,
            request.Limit,
            e => e.EventId == request.EventId,
            null,
            false,
            cancellationToken);

        tickets = tickets.OrderBy(e => e.Seat.Sector).ThenBy(e => e.Seat.Row).ThenBy(e => e.Seat.SeatNumber).ToArray();

        return _mapper.Map<IReadOnlyCollection<GetTicketDto>>(tickets);
    }
}
