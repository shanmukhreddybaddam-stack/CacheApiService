using CachedApiService.Models;
using Microsoft.Extensions.Hosting;

namespace CachedApiService.Services
{
    public class BackgroundDataLoader : BackgroundService, IBackgroundDataLoader
    {
        private readonly ExternalApiService _externalService;
        private readonly CacheService _cache;
        private readonly ILogger<BackgroundDataLoader> _logger;

        public BackgroundDataLoader(
            ExternalApiService externalService,
            CacheService cache,
            ILogger<BackgroundDataLoader> logger)
        {
            _externalService = externalService;
            _cache = cache;
            _logger = logger;
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

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Schedule every 5 min
            }
        }

    }
}
