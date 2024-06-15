using Microsoft.Extensions.Caching.Distributed;
using TicketEventSearch.Application.Abstractions.Caches.Category;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Infrastructure.DistributedCache.Categories;

public class CategoryCache : BaseCache<GetCategoryDto>, ICategoryCache
{
    public CategoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
