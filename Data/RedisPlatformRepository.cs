using System.Text.Json;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data;

public class RedisPlatformRepository : IPlatformRepository
{
    private readonly IConnectionMultiplexer _redis;

    public RedisPlatformRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }
    public void CreatePlatform(Platform plat)
    {
        if (plat == null)
        {
            throw new ArgumentOutOfRangeException(nameof(plat));
        }

        var db = _redis.GetDatabase();
        var serialPlat = JsonSerializer.Serialize(plat);

        db.HashSet($"hashplatform", new HashEntry[]
            {new HashEntry(plat.Id, serialPlat)});
    }

    public Platform? GetPlatform(string id)
    {
        var db = _redis.GetDatabase();
        var plat = db.HashGet("hashplatform", id);

        if(!string.IsNullOrEmpty(plat))
        {
            return JsonSerializer.Deserialize<Platform>(plat);
        }
        return null;
    }

    public IEnumerable<Platform?>? GetPlatforms()
    {
        var db = _redis.GetDatabase();
        var completeSet = db.HashGetAll("hashplatform");

        if (completeSet.Length > 0)
        {
            var obj = Array.ConvertAll(completeSet, val =>
                JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
            return obj;
        }
        return null;
    }
}