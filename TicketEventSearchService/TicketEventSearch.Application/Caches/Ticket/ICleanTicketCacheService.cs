namespace TicketEventSearch.Application.Caches.Ticket;

public interface ICleanTicketCacheService
{
    void ClearAllTicketCaches();
    void ClearListTicketCaches();
}