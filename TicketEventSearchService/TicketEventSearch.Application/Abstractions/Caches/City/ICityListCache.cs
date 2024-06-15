using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Abstractions.Caches.City;

public interface ICityListCache : IBaseCache<IReadOnlyCollection<GetCityDto>>
{

}
