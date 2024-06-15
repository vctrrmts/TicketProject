using MediatR;

namespace Notification.Application.Handlers.Commands.CreateEvent;

public class CreateEventCommand : IRequest
{
    public Guid EventId { get; set; }
}
