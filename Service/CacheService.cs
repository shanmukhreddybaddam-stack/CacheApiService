using CachedApiService.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CachedApiService.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "ProductsCache";

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        // public void Set(String key, List<Product> data)
        // {
        //     _cache.Set(key, data, TimeSpan.FromMinutes(10));
        // }

        // public List<Product>? Get(String key)
        // {
        //     return _cache.TryGetValue(key, out List<Product>? cachedData)
        //         ? cachedData
        //         : null;
        // }

        public void Set<T>(string key, List<T> data)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(10));
        }

        public List<T>? Get<T>(string key)
        {
            _cache.TryGetValue(key, out List<T>? data);
            return data;
        }
    }
}
