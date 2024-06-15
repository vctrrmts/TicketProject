namespace TicketEventManagement.Application.DTOs;

public class GetTicketDto
{
    public Guid TicketId { get; set; }
    public Guid EventId { get; set; } 
    public Guid SeatId { get; set; }
    public double? Price { get; set; }
}
