namespace TicketEventSearch.Application.Caches.Event;

internal interface ICleanEventCacheService
{
    void ClearAllEventCaches();
    void ClearListEventCaches();
}