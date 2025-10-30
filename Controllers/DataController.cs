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
        public IActionResult GetProducts([FromQuery] int? id)
        {
            var data = _cacheService.Get<Product>("Products");
            if (data == null || !data.Any())
                return NotFound("Cache is empty or not yet populated.");

            if (id.HasValue)
            {
                data = data.Where(p => p.Id == id.Value).ToList();
                if (!data.Any())
                    return NotFound($"No product found with ID {id.Value}.");
            }

            return Ok(data);
        }

        [HttpGet("users")]
        public IActionResult GetUsers([FromQuery] int? id, [FromQuery] string? email)
        {
            var data = _cacheService.Get<User>("Users");
            if (data == null || !data.Any())
                return NotFound("Cache is empty or not yet populated.");

            // Filter by id
            if (id.HasValue)
                data = data.Where(u => u.Id == id.Value).ToList();

            // Filter by email (case-insensitive)
            if (!string.IsNullOrEmpty(email))
                data = data.Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!data.Any())
                return NotFound("No users match the filter criteria.");

            return Ok(data);
        }


        [HttpGet("posts")]
        public IActionResult GetPosts([FromQuery] int? userId, [FromQuery] int? postId)
        {
            var data = _cacheService.Get<Post>("Posts");
            if (data == null || !data.Any())
                return NotFound("Cache is empty or not yet populated.");

            // Filter by userId
            if (userId.HasValue)
            {
                data = data.Where(p => p.UserId == userId.Value).ToList();
                if (!data.Any())
                    return NotFound($"No posts found for user with ID {userId.Value}.");
            }

            // Filter by postId
            if (postId.HasValue)
            {
                data = data.Where(p => p.Id == postId.Value).ToList();
                if (!data.Any())
                    return NotFound($"No posts found with ID {postId.Value}.");
            }

            return Ok(data);
        }

    }
}
