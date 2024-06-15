namespace Notification.Domain;

public class BuyedTicket
{
    public Guid TicketId { get; set; }
    public Guid EventId { get; set; }
    public double Price { get; set; }
    public string ClientMail { get; set; } = default!;

}
