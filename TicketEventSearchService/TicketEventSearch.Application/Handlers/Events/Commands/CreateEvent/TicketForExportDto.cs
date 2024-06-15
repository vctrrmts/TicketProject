namespace TicketEventSearch.Application.Handlers.Events.Commands.CreateEvent;

public class TicketForExportDto
{
    public Guid TicketId { get; set; }
    public Guid EventId { get; set; }
    public Guid SeatId { get; set; }
    public double Price { get; set; }
}
