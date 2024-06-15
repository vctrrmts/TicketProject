using Microsoft.Extensions.Caching.Distributed;
using TicketEventSearch.Application.Abstractions.Caches.Seat;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Infrastructure.DistributedCache.Seats;

public class SeatsListCache : BaseCache<IReadOnlyCollection<GetSeatDto>>, ISeatListCache
{
    public SeatsListCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
