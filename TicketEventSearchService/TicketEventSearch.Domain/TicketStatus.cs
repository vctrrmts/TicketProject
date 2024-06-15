namespace TicketEventSearch.Domain;

public class TicketStatus
{
    public int TicketStatusId { get; private set; }
    public string Name { get; private set; } = default!;
    public virtual IReadOnlyCollection<Ticket> Tickets { get; private set; }
}
