using Microsoft.Extensions.Caching.Distributed;
using TicketEventSearch.Application.Abstractions.Caches.Seat;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Infrastructure.DistributedCache.Seats;

public class SeatCache : BaseCache<GetSeatDto>, ISeatCache
{
    public SeatCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
