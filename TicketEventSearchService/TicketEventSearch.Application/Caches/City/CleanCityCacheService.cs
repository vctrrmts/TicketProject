using TicketEventSearch.Application.Abstractions.Caches.City;

namespace TicketEventSearch.Application.Caches.City;

public class CleanCityCacheService : ICleanCityCacheService
{
    private readonly ICityCache _cityCache;
    private readonly ICityListCache _cityListCache;

    public CleanCityCacheService(ICityListCache cityListCache, ICityCache cityCache)
    {
        _cityListCache = cityListCache;
        _cityCache = cityCache;
    }

    public void ClearAllCityCaches()
    {
        _cityCache.Clear();
        _cityListCache.Clear();
    }

    public void ClearListCityCaches()
    {
        _cityListCache.Clear();
    }
}
