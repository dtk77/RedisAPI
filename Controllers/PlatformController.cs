using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IPlatformRepository _repo;

    public PlatformController(IPlatformRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Platform>> GetAllPlatform()
    {
        return Ok(_repo.GetPlatforms());
    }


    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<Platform> GetPlatformById(string id)
    {
        var platform = _repo.GetPlatform(id);
        if (platform != null)
        {
            return platform;
        }
        return NotFound();
    }

    [HttpPost]
    public ActionResult CreatePlatform(Platform platform)
    {
        _repo.CreatePlatform(platform);

        return CreatedAtRoute(nameof(GetPlatformById),
                                new { Id = platform.Id }, platform);
    }
}
