using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.OrderService.Application.Interfaces;
using ShopSphere.OrderService.Infrastructure.Data;
using ShopSphere.OrderService.Infrastructure.Repositories;

namespace ShopSphere.OrderService.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString(
                    "DefaultConnection")));
        services.AddScoped<IOrderRepository,OrderRepository>();
        services.AddJwtAuthentication(configuration);

        return services;
    }
}