namespace TicketBuying.Domain;

public class Seat
{
    public string Sector { get; set; } = default!;
    public int? Row { get; set; }
    public int SeatNumber { get; set; }
}
