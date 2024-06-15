namespace TicketEventSearch.Application.Caches.Category;

internal interface ICleanCategoryCacheService
{
    void ClearListCategoryCaches();
    void ClearAllCategoryCaches();
}
