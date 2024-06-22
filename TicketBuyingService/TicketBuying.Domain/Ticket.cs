namespace TicketBuying.Domain;

public class Ticket
{
    public Guid TicketId { get; set; }
    public Guid EventId { get; set; }
    public double Price { get; set; }
    public string Mail { get; set; } = default!;
    public string HashGuid { get; set; } = default!;
    public virtual Event Event { get; set; } = default!;
    public virtual Seat Seat { get; set; } = default!;

}
