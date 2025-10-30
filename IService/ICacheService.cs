public interface ICacheService
{
    void Set<T>(string key, List<T> data);
    List<T>? Get<T>(string key);
}
