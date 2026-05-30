using Microsoft.Extensions.Caching.Distributed;
using ShopSphere.ProductService.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace ShopSphere.ProductService.Infrastructure.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly IConnectionMultiplexer _redis;

    public RedisCacheService(IDistributedCache cache,IConnectionMultiplexer redis)
    {
        _cache = cache;
        _redis = redis;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var cachedData = await _cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(cachedData))
            return default;

        return JsonSerializer.Deserialize<T>(cachedData);
    }

    public async Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiry = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow =
                expiry ?? TimeSpan.FromMinutes(5)
        };

        var json = JsonSerializer.Serialize(value);

        await _cache.SetStringAsync(
            key,
            json,
            options);
    }
    public async Task RemoveByPrefixAsync(string prefix)
    {
        var endpoint = _redis.GetEndPoints().First();

        var server = _redis.GetServer(endpoint);

        var database = _redis.GetDatabase();

        foreach (var key in server.Keys(pattern: $"{prefix}*"))
        {
            await database.KeyDeleteAsync(key);
        }
    }
    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}