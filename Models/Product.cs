namespace CachedApiService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Brand { get; set; } = string.Empty;
    }
}
