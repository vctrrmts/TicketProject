using Microsoft.Extensions.Caching.Distributed;
using TicketEventSearch.Application.Abstractions.Caches.City;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Infrastructure.DistributedCache.Cities;

public class CityListCache : BaseCache<IReadOnlyCollection<GetCityDto>>, ICityListCache
{
    public CityListCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
