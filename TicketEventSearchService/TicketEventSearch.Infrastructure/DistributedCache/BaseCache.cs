using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using TicketEventSearch.Application.Abstractions;
using TicketEventSearch.Infrastructure.Extensions;

namespace TicketEventSearch.Infrastructure.DistributedCache;

public abstract class BaseCache<TItem> : IBaseCache<TItem>
{
    private readonly IDistributedCache _distributedCache;

    private readonly RedisService _redisServer;

    private string ItemName => TypeExtensions.GetFormattedName(typeof(TItem));

    public BaseCache(IDistributedCache distributedCache, RedisService redisServer)
    {
        _distributedCache = distributedCache;
        _redisServer = redisServer;
    }

    protected virtual int AbsoluteExpiration => 10;

    protected virtual int SlidingExpiration => 5;

    public IDistributedCache DistributedCache { get; }
    public IServer RedisServer { get; }

    private string CreateCacheKey<TRequest>(TRequest request)
    {
        return $"{ItemName}_{JsonSerializer.Serialize(request)}";
    }

    private string CreateCacheKey<TRequest>(TRequest request, string secondKey)
    {
        return $"{ItemName}_{JsonSerializer.Serialize(request)}_{secondKey}";
    }

    public void Set<TRequest>(TRequest request, string secondKey, TItem item, int size)
    {
        var jsonItem = JsonSerializer.Serialize(item);

        _distributedCache.SetString(CreateCacheKey(request, secondKey), jsonItem, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(AbsoluteExpiration),
            SlidingExpiration = TimeSpan.FromMinutes(SlidingExpiration)
        });
    }

    public void Set<TRequest>(TRequest request, TItem item, int size)
    {
        var jsonItem = JsonSerializer.Serialize(item);

        _distributedCache.SetString(CreateCacheKey(request), jsonItem, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(AbsoluteExpiration),
            SlidingExpiration = TimeSpan.FromMinutes(SlidingExpiration)
        });
    }

    public bool TryGetValue<TRequest>(TRequest request, out TItem? item)
    {
        var itemString = _distributedCache.GetString(CreateCacheKey(request));
        if (string.IsNullOrWhiteSpace(itemString))
        {
            item = default;
            return false;
        }

        item = JsonSerializer.Deserialize<TItem>(itemString, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = false
        });
        return true;
    }

    public bool TryGetValue<TRequest>(TRequest request, string secondKey, out TItem? item)
    {
        var itemString = _distributedCache.GetString(CreateCacheKey(request, secondKey));
        if (string.IsNullOrWhiteSpace(itemString))
        {
            item = default;
            return false;
        }

        item = JsonSerializer.Deserialize<TItem>(itemString, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = false
        });
        return true;
    }

    public void DeleteItem<TRequest>(TRequest request)
    {
        _distributedCache.Remove(CreateCacheKey(request));
    }

    public void DeleteItem<TRequest>(TRequest request, string secondKey)
    {
        _distributedCache.Remove(CreateCacheKey(request, secondKey));
    }

    public void Clear()
    {
        var keys = _redisServer.GetAllKeys(ItemName).ToArray();

        foreach (var key in keys)
        {
            _distributedCache.Remove(key);
        }
    }

    public void Clear(string keyPostfix)
    {
        var keys = _redisServer.GetAllKeys(ItemName, keyPostfix).ToArray();

        foreach (var key in keys)
        {
            _distributedCache.Remove(key);
        }
    }
}