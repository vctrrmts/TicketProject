using Microsoft.Extensions.Caching.Distributed;
using TicketEventSearch.Application.Abstractions.Caches.Location;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Infrastructure.DistributedCache.Locations;

public class LocationCache : BaseCache<GetLocationDto>, ILocationCache
{
    public LocationCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
