namespace TicketEventSearch.Application.Caches.Event;

public interface ICleanEventCacheService
{
    void ClearAllEventCaches();
    void ClearListEventCaches();
}