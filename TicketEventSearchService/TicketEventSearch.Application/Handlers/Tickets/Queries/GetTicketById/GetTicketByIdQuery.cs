using MediatR;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketById;

public class GetTicketByIdQuery : IRequest<GetTicketDto>
{
    public Guid TicketId { get; set; }
}
