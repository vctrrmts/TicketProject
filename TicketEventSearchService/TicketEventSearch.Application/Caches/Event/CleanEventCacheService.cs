using TicketEventSearch.Application.Abstractions.Caches.Event;

namespace TicketEventSearch.Application.Caches.Event;

internal class CleanEventCacheService : ICleanEventCacheService
{
    private readonly IEventCache _eventCache;

    private readonly IEventListCache _eventListCache;


    public CleanEventCacheService(
        IEventCache eventCache,
        IEventListCache eventListCache)
    {
        _eventCache = eventCache;
        _eventListCache = eventListCache;
    }

    public void ClearAllEventCaches()
    {
        _eventCache.Clear();
        _eventListCache.Clear();
    }

    public void ClearListEventCaches()
    {
        _eventListCache.Clear();
    }
}