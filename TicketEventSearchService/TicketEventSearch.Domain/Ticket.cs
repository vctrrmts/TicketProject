using TicketEventSearch.Domain.Enums;

namespace TicketEventSearch.Domain;

public class Ticket
{
    public Guid TicketId { get; private set; }
    public Guid EventId { get; private set; }
    public Guid SeatId { get; private set; }
    public double Price { get; private set; }
    public int TicketStatusId {  get; private set; }
    public DateTime? UnavailableStatusEnd { get; private set; }
    public virtual TicketStatus Status { get; private set; }
    public virtual Event Event { get; private set; }
    public virtual Seat Seat { get; private set; }

    private Ticket() { }

    public Ticket(Guid ticketId, Guid eventId, Guid seatId, double price)
    {
        if (string.IsNullOrWhiteSpace(ticketId.ToString()))
        {
            throw new ArgumentException("Incorrect TicketId", nameof(ticketId));
        }

        if (string.IsNullOrWhiteSpace(eventId.ToString()))
        {
            throw new ArgumentException("Incorrect EventId", nameof(eventId));
        }

        if (string.IsNullOrWhiteSpace(seatId.ToString()))
        {
            throw new ArgumentException("Incorrect SeatId", nameof(seatId));
        }

        if (price <= 0)
        {
            throw new ArgumentException("Incorrect Price", nameof(price));
        }

        TicketId = ticketId;
        EventId = eventId;
        SeatId = seatId;
        Price = price;
        TicketStatusId = 1;
    }

    public void UpdateStatusId(int statusId)
    {
        if (!Enum.IsDefined(typeof(TicketStatusEnum), statusId))
        {
            throw new ArgumentException("Incorrect Status Id", nameof(statusId));
        }

        TicketStatusId = statusId;
    }

    public void UpdateUnavailableStatusEnd(DateTime? dateTimeEnd)
    {
        if (dateTimeEnd is not null && dateTimeEnd < DateTime.UtcNow)
        {
            throw new ArgumentException("Incorrect Unavailable Status end", nameof(dateTimeEnd));
        }

        UnavailableStatusEnd = dateTimeEnd;
    }
}
