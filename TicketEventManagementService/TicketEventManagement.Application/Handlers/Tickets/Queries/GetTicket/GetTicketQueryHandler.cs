using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Tickets.Queries.GetTicket;

internal class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, GetTicketDto>
{
    private readonly IBaseRepository<Ticket> _tickets;

    private readonly IMapper _mapper;

    public GetTicketQueryHandler(IBaseRepository<Ticket> tickets, IMapper mapper)
    {
        _tickets = tickets;
        _mapper = mapper;
    }

    public async Task<GetTicketDto> Handle(GetTicketQuery request, CancellationToken cancellationToken)
    {
        Ticket? ticket = await _tickets.SingleOrDefaultAsync(x => x.TicketId == request.TicketId);
        if (ticket is null)
        {
            throw new NotFoundException($"Ticket with TicketId = {request.TicketId} not found");
        }

        return _mapper.Map<GetTicketDto>(ticket);
    }
}
