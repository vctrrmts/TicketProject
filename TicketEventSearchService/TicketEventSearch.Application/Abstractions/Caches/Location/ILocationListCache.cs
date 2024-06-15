using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Abstractions.Caches.Location;

public interface ILocationListCache : IBaseCache<IReadOnlyCollection<GetLocationDto>>
{
}
