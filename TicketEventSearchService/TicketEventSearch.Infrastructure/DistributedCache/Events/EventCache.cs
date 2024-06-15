using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using TicketEventSearch.Application.Abstractions.Caches.Event;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Infrastructure.DistributedCache.Events;

public class EventCache : BaseCache<GetEventDto>, IEventCache
{
    public EventCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
