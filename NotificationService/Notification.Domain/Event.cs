namespace Notification.Domain;

public class Event
{
    public Guid EventId { get; private set; } = default!;

    private Event() { }

    public Event(Guid eventId)
    {
        if (string.IsNullOrEmpty(eventId.ToString()))
        {
            throw new ArgumentException("EventId is empty", nameof(eventId));
        }
        EventId = eventId;
    }
}
