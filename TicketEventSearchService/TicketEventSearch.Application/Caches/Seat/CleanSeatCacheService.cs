using TicketEventSearch.Application.Abstractions.Caches.Seat;

namespace TicketEventSearch.Application.Caches.Seat;

internal class CleanSeatCacheService : ICleanSeatCacheService
{
    private readonly ISeatCache _seatCache;

    private readonly ISeatListCache _seatListCache;

    public CleanSeatCacheService(
        ISeatListCache seatListCache, 
        ISeatCache seatCache)
    {
        _seatListCache = seatListCache;
        _seatCache = seatCache;
    }

    public void ClearAllSeatCaches()
    {
        _seatCache.Clear();
        _seatListCache.Clear();
    }

    public void ClearListSeatCaches()
    {
        _seatListCache.Clear();
    }
}
