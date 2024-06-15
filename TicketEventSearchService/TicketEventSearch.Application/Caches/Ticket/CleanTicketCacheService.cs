using TicketEventSearch.Application.Abstractions.Caches.Ticket;

namespace TicketEventSearch.Application.Caches.Ticket;

internal class CleanTicketCacheService : ICleanTicketCacheService
{
    private readonly ITicketCache _ticketCache;

    private readonly ITicketListCache _ticketListCache;

    public CleanTicketCacheService(
        ITicketCache ticketCache,
        ITicketListCache ticketListCache)
    {
        _ticketCache = ticketCache;
        _ticketListCache = ticketListCache;
    }

    public void ClearAllTicketCaches()
    {
        _ticketCache.Clear();
        _ticketListCache.Clear();
    }

    public void ClearListTicketCaches()
    {
        _ticketListCache.Clear();
    }
}