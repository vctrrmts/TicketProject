using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Tickets.Queries.GetListTickets;

internal class GetListTicketsQueryHandler : IRequestHandler<GetListTicketsQuery, IReadOnlyCollection<GetTicketDto>>
{
    private readonly IBaseRepository<Ticket> _tickets;

    private readonly IBaseRepository<Event> _events;

    private readonly IMapper _mapper;

    public GetListTicketsQueryHandler(IBaseRepository<Ticket> tickets, IBaseRepository<Event> events, IMapper mapper)
    {
        _tickets = tickets;
        _events = events;
        _mapper = mapper;
    }
    public async Task<IReadOnlyCollection<GetTicketDto>> Handle(GetListTicketsQuery request, 
        CancellationToken cancellationToken)
    {
        if (request.EventId is not null)
        {
            Event? myEvent = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId);
            if (myEvent is null)
            {
                throw new NotFoundException($"Event with EventId = {request.EventId} not found");
            }
        }

        var tickets = await _tickets.GetListAsync(
            request.Offset,
            request.Limit,
            e => ((request.EventId == null) || (e.EventId == request.EventId)),
            null,
            false,
            cancellationToken);

        return _mapper.Map<IReadOnlyCollection<GetTicketDto>>(tickets);
    }
}
