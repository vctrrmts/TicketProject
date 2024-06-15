namespace TicketEventManagement.Application.DTOs;

public class TicketForExportDto
{
    public Guid TicketId { get; private set; }
    public Guid EventId { get; private set; }
    public Guid SeatId { get; private set; }
    public double Price { get; private set; }
}
