using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.ProductService.Application.Interfaces;
using ShopSphere.ProductService.Infrastructure.Data;
using ShopSphere.ProductService.Infrastructure.Repositories;
using ShopSphere.ProductService.Infrastructure.Services;
using StackExchange.Redis;

namespace ShopSphere.ProductService.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString(
                    "DefaultConnection")));

        services.AddScoped<IProductRepository,
            ProductRepository>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration =
                configuration["Redis:ConnectionString"];
        });
        services.AddSingleton<IConnectionMultiplexer>(_ =>
                            ConnectionMultiplexer.Connect(
                            configuration["Redis:ConnectionString"]!));

        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}