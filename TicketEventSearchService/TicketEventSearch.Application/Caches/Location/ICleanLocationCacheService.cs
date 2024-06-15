namespace TicketEventSearch.Application.Caches.Location;

internal interface ICleanLocationCacheService
{
    void ClearListLocationCaches();
    void ClearAllLocationCaches();
}
