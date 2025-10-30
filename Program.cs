using CachedApiService.Configurations;
using CachedApiService.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<ExternalApiService>();
builder.Services.AddSingleton<CacheService>();
builder.Services.AddHostedService<BackgroundDataLoader>();
builder.Services.Configure<ExternalApiOptions>(
builder.Configuration.GetSection("ExternalApi"));

builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>();
builder.Services.AddHttpClient<IBackgroundDataLoader, BackgroundDataLoader>();
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1.json");
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
