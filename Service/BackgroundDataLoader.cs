using CachedApiService.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CachedApiService.Services
{
    public class BackgroundDataLoader : BackgroundService, IBackgroundDataLoader
    {
        private readonly ExternalApiService _externalService;
        private readonly CacheService _cache;
        private readonly ILogger<BackgroundDataLoader> _logger;
        private readonly int _refreshIntervalMinutes;
        public BackgroundDataLoader(
            ExternalApiService externalService,
            CacheService cache,
            ILogger<BackgroundDataLoader> logger,
            IOptions<CacheSettings> cacheSettings)
        {
            _externalService = externalService;
            _cache = cache;
            _logger = logger;
            _refreshIntervalMinutes = cacheSettings.Value.RefreshIntervalMinutes;
        }

        public async Task LoadDataAsync(CancellationToken cancellationToken)
        {
            var products = await _externalService.GetProductsAsync();
            var users = await _externalService.GetUsersAsync();
            var posts = await _externalService.GetPostsAsync();

            _cache.Set("Products", products);
            _cache.Set("Users", users);
            _cache.Set("Posts", posts);

            _logger.LogInformation("Cache refreshed at: {time}", DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await LoadDataAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error refreshing cache.");
                }

                await Task.Delay(TimeSpan.FromMinutes(_refreshIntervalMinutes), stoppingToken); // Schedule every 5 min
            }
        }

    }
}
