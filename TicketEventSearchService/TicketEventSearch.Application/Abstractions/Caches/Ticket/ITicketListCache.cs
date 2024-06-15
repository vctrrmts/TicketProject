using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Abstractions.Caches.Ticket;

public interface ITicketListCache : IBaseCache<IReadOnlyCollection<GetTicketDto>>
{
}
