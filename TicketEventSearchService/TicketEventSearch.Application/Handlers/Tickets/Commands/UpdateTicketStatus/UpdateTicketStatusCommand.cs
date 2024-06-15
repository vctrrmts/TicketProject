using MediatR;

namespace TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus
{
    public class UpdateTicketStatusCommand : IRequest<GetTicketForSentMailDto>
    {
        public Guid TicketId { get; set; }
        public int TicketStatusId { get; set; }
    }
}
