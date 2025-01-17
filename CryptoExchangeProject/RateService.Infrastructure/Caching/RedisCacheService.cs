using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RateService.Application.Interfaces;
using StackExchange.Redis;

namespace RateService.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDistributedCache _cache;
    private readonly IDatabase _database;
    
    public RedisCacheService(){}

    public RedisCacheService(IConnectionMultiplexer redis,IDistributedCache cache)
    {
        _redis = redis;
        _database = _redis.GetDatabase();
        _cache = cache;
    }

    public async Task SetAsync(string key, string value, TimeSpan expiration)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        await _cache.SetStringAsync(key, value, options);
    }

    public async Task<string> GetAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }
}