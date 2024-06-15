namespace TicketControlService.Domain;

public class Ticket
{
    public Guid TicketId { get; set; }
    public Guid EventId { get; set; }
    public string HashGuid { get; set; } = default!;

}
