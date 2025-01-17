using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateService.Application.Interfaces;
using RateService.Infrastructure.Caching;
using StackExchange.Redis;

namespace RateService.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do Redis
        var redisConnection = configuration.GetSection("Redis")["ConnectionString"];
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));

        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddHttpClient<IExchangeRateService, BinanceIntegrationService>(client =>
        {
            client.BaseAddress = new Uri("https://api.binance.com");
        });

        return services;
    }
}