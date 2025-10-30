using CachedApiService.Configurations;
using CachedApiService.Models;
using Microsoft.Extensions.Options;

namespace CachedApiService.Services
{
    public class ExternalApiService: IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ExternalApiOptions _options;

        public ExternalApiService(HttpClient httpClient, IOptions<ExternalApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;

            // Setting the base URL dynamically
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ProductResponse>(_options.Endpoints.Products);
            return response?.Products ?? new List<Product>();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<UserResponse>(_options.Endpoints.Users);
            return response?.Users ?? new List<User>();
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<PostResponse>(_options.Endpoints.Posts);
            return response?.Posts ?? new List<Post>();
        }
    }
}
