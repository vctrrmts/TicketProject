using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Events.Queries.GetEvent;

public class GetEventQuery : IRequest<GetEventDto>
{
    public Guid EventId { get; set; }
}
