using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Abstractions.Caches.Seat;

public interface ISeatListCache : IBaseCache<IReadOnlyCollection<GetSeatDto>>
{
}
