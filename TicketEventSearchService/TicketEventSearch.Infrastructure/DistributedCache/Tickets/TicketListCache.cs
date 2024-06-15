using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using TicketEventSearch.Application.Abstractions.Caches.Ticket;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Infrastructure.DistributedCache.Tickets;

public class TicketListCache : BaseCache<IReadOnlyCollection<GetTicketDto>>, ITicketListCache
{
    public TicketListCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
