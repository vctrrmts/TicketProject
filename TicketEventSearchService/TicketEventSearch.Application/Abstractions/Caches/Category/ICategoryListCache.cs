using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Abstractions.Caches.Category;

public interface ICategoryListCache : IBaseCache<IReadOnlyCollection<GetCategoryDto>>
{
}
