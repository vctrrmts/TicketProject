using MediatR;

namespace TicketEventManagement.Application.Handlers.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketsCommand : IRequest
    {
        public Guid EventId { get; set; }
    }
}
