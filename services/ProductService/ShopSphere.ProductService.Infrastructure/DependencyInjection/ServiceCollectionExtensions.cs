using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.ProductService.Application.Interfaces;
using ShopSphere.ProductService.Infrastructure.Data;
using ShopSphere.ProductService.Infrastructure.Repositories;

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

        return services;
    }
}