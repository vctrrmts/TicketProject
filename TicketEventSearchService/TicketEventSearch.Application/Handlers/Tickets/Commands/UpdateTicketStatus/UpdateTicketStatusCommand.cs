using MediatR;

namespace TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus
{
    public class UpdateTicketStatusCommand : IRequest<IReadOnlyCollection<GetTicketForSentMailDto>>
    {
        public Guid[] TicketIds { get; set; } = default!;
        public int TicketStatusId { get; set; }
    }
}
