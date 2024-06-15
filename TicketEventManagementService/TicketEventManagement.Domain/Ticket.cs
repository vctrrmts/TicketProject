namespace TicketEventManagement.Domain;

public class Ticket
{
    public Guid TicketId { get; private set; }
    public Guid EventId { get; private set; }
    public Guid SeatId { get; private set; }
    public double? Price { get; private set; }
    public virtual Event Event { get; private set; }
    public virtual Seat Seat { get; private set; }

    private Ticket() { }

    public Ticket(Guid eventId, Guid seatId, double? price)
    {
        if (string.IsNullOrWhiteSpace(eventId.ToString()))
        {
            throw new ArgumentException("Incorrect EventId", nameof(eventId));
        }

        if (string.IsNullOrWhiteSpace(seatId.ToString()))
        {
            throw new ArgumentException("Incorrect SeatId", nameof(seatId));
        }

        if (price is not null && price <= 0)
        {
            throw new ArgumentException("Incorrect Price", nameof(price));
        }

        EventId = eventId;
        SeatId = seatId;
        Price = price;
    }

    public void UpdatePrice(double price) 
    {
        if (price <= 0)
        {
            throw new ArgumentException("Incorrect Price", nameof(price));
        }

        Price = price;
    }
}
