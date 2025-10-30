using CachedApiService.Models;
using CachedApiService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CachedApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly CacheService _cacheService;

        public DataController(CacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            var data = _cacheService.Get<Product>("Products");
            if (data == null || !data.Any())
                return NotFound("Cache is empty or not yet populated.");

            return Ok(data);
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var data = _cacheService.Get<User>("Users");
            if (data == null || !data.Any())
                return NotFound("Cache is empty or not yet populated.");

            return Ok(data);
        }

        [HttpGet("posts")]
        public IActionResult GetPosts()
        {
            var data = _cacheService.Get<Post>("Posts");
            if (data == null || !data.Any())
                return NotFound("Cache is empty or not yet populated.");

            return Ok(data);
        }
    }
}
