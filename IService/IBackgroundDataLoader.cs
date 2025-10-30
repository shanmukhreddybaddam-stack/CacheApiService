namespace CachedApiService.Services
{
    public interface IBackgroundDataLoader
    {
        Task LoadDataAsync(CancellationToken cancellationToken);
    }
}
