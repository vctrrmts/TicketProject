using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Abstractions.Caches.Event;

public interface IEventListCache : IBaseCache<IReadOnlyCollection<GetEventDto>>
{
}
