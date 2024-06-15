using MediatR;

namespace TicketEventManagement.Application.Handlers.Events.Commands.PublishEvent;

public class PublishEventCommand : IRequest
{
    public Guid EventId { get; set; }
}
