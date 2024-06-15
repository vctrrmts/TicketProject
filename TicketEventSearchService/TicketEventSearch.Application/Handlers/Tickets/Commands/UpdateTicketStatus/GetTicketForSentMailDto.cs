using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus;

public class GetTicketForSentMailDto
{
    public Guid TicketId { get; set; }
    public Guid EventId { get; set; }
    public double? Price { get; set; }
    public virtual GetEventDto Event { get; set; }
    public virtual GetSeatDto Seat { get; set; }
}
