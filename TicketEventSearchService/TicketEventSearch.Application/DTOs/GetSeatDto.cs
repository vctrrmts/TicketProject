namespace TicketEventSearch.Application.DTOs;

public class GetSeatDto
{
    public Guid SeatId { get; set; }
    public string Sector { get; set; } = default!;
    public int? Row { get; set; }
    public int SeatNumber { get; set; }
}
