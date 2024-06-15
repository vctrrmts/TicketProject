using AutoMapper;
using TicketEventSearch.Application.Abstractions.Caches.Ticket;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketById;

public class GetTicketByIdQueryHandler : BaseCashedQuery<GetTicketByIdQuery, GetTicketDto>
{
    private readonly IBaseRepository<Ticket> _tickets;
    private readonly IMapper _mapper;

    public GetTicketByIdQueryHandler(IBaseRepository<Ticket> tickets, IMapper mapper, 
        ITicketCache cache) : base(cache)
    {
        _tickets = tickets;
        _mapper = mapper;
    }

    public override async Task<GetTicketDto> SentQueryAsync(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        Ticket? ticket = await _tickets.SingleOrDefaultAsync(x => x.TicketId == request.TicketId, cancellationToken);
        if (ticket == null)
        {
            throw new NotFoundException($"Ticket with id = {request.TicketId} not found");
        }

        return _mapper.Map<GetTicketDto>(ticket);
    }
}
