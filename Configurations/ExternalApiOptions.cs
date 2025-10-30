namespace CachedApiService.Configurations
{
    public class ExternalApiOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public EndpointsConfig Endpoints { get; set; } = new EndpointsConfig();
    }

    public class EndpointsConfig
    {
        public string Products { get; set; } = string.Empty;
        public string Users { get; set; } = string.Empty;
        public string Posts { get; set; } = string.Empty;
    }
}
