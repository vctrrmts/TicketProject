using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Tickets.Queries.GetTicket;

public class GetTicketQuery : IRequest<GetTicketDto>
{
    public Guid TicketId { get; set; }
}
