using TicketEventSearch.Application.Abstractions.Caches.Location;

namespace TicketEventSearch.Application.Caches.Location;

public class CleanLocationCacheService : ICleanLocationCacheService
{
    private readonly ILocationCache _locationCache;
    private readonly ILocationListCache _locationListCache;

    public CleanLocationCacheService(
        ILocationListCache locationListCache, 
        ILocationCache locationCache)
    {
        _locationListCache = locationListCache;
        _locationCache = locationCache;
    }

    public void ClearAllLocationCaches()
    {
        _locationCache.Clear();
        _locationListCache.Clear();
    }

    public void ClearListLocationCaches()
    {
        _locationListCache.Clear();
    }
}
