using RedisAPI.Models;

namespace RedisAPI.Data;

public interface IPlatformRepository
{
    void CreatePlatform(Platform plat);
    Platform? GetPlatform(string id);
    IEnumerable<Platform?>? GetPlatforms();
}