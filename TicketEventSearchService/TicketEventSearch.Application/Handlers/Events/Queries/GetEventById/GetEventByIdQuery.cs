using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Events.Queries.GetEventById;

public class GetEventByIdQuery : IRequest<GetEventDto>
{
    public Guid EventId { get; set; }
}
