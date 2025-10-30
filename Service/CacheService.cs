using CachedApiService.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CachedApiService.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheService> _logger;
        private readonly int _cacheExpirationMinutes;

        public CacheService(
            IMemoryCache cache,
            IOptions<CacheSettings> cacheSettings,
            ILogger<CacheService> logger)
        {
            _cache = cache;
            _cacheExpirationMinutes = cacheSettings.Value.CacheExpirationMinutes;
            _logger = logger;
        }

        public void Set<T>(string key, List<T> data)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(_cacheExpirationMinutes));
            _logger.LogInformation("Cache set for key '{Key}' with {Count} items, expiration: {Minutes} minutes.",
                key, data.Count, _cacheExpirationMinutes);
        }

        public List<T>? Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out List<T>? data))
            {
                _logger.LogInformation("Cache hit for key '{Key}', items retrieved: {Count}.", key, data?.Count ?? 0);
                return data;
            }
            else
            {
                _logger.LogWarning("Cache miss for key '{Key}'.", key);
                return null;
            }
        }
    }
}
