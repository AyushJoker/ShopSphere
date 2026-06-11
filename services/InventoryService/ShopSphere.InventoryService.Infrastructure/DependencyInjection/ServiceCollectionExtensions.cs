using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.InventoryService.Application.Interfaces;
using ShopSphere.InventoryService.Infrastructure.Data;
using ShopSphere.InventoryService.Infrastructure.Repositories;


namespace ShopSphere.InventoryService.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString(
                    "DefaultConnection")));



        services.AddScoped<IInventoryRepository,InventoryRepository>();

        services.AddJwtAuthentication(configuration);

        return services;
    }
}