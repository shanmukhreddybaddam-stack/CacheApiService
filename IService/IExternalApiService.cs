using CachedApiService.Models;

namespace CachedApiService.Services
{
    public interface IExternalApiService
    {
        Task<List<Product>> GetProductsAsync();
        Task<List<User>> GetUsersAsync();
        Task<List<Post>> GetPostsAsync();
    }
}
