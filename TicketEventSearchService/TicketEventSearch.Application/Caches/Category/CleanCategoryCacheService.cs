using TicketEventSearch.Application.Abstractions.Caches.Category;

namespace TicketEventSearch.Application.Caches.Category;

public class CleanCategoryCacheService : ICleanCategoryCacheService
{
    private readonly ICategoryCache _categoryCache;
    private readonly ICategoryListCache _categoryListCache;

    public CleanCategoryCacheService(ICategoryListCache categoryListCache, ICategoryCache categoryCache)
    {
        _categoryListCache = categoryListCache;
        _categoryCache = categoryCache;
    }
    public void ClearAllCategoryCaches()
    {
        _categoryCache.Clear();
        _categoryListCache.Clear();
    }

    public void ClearListCategoryCaches()
    {
        _categoryListCache.Clear();
    }
}
