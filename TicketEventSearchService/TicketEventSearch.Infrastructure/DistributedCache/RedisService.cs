using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace TicketEventSearch.Infrastructure.DistributedCache;

public class RedisService
{
    private readonly IConfiguration _configuration;

    public IDatabase db { get; set; }

    public RedisService(IConfiguration configuration) 
    {
        _configuration = configuration;
    }

    public RedisKey[] GetAllKeys(string keyPrefix)
    {
        using var redis = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis"));
        var server = redis.GetServers().Single();
        return server.Keys(pattern: keyPrefix + "*").ToArray();
    }

    public RedisKey[] GetAllKeys(string keyPrefix, string keyPostfix)
    {
        using var redis = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis"));
        var server = redis.GetServers().Single();
        return server.Keys(pattern: keyPrefix + "*" + keyPostfix).ToArray();
    }
}
