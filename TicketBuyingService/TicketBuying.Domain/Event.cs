namespace TicketBuying.Domain;

public class Event
{
    public Guid EventId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string UriMainImage { get; set; } = default!;
    public DateTime DateTimeEventStart { get; set; }
    public DateTime DateTimeEventEnd { get; set; }
    public Location Location { get; set; } = default!;
}
