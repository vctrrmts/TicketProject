namespace TicketEventSearch.Application.Caches.Location;

public interface ICleanLocationCacheService
{
    void ClearListLocationCaches();
    void ClearAllLocationCaches();
}
