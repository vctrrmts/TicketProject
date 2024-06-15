using MediatR;

namespace TicketEventManagement.Application.Handlers.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest
    {
        public Guid EventId { get; set; }
    }
}
