using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.OrderService.Application.Interfaces;
using ShopSphere.OrderService.Infrastructure.Data;
using ShopSphere.OrderService.Infrastructure.Http;
using ShopSphere.OrderService.Infrastructure.Repositories;
using ShopSphere.OrderService.Infrastructure.Services;

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

        services.AddTransient<JwtForwardingHandler>();

        services.AddJwtAuthentication(configuration);

        
        services.AddHttpClient<IProductServiceClient, ProductServiceClient>(client => {
            client.BaseAddress =
                new Uri(configuration["Services:ProductService"]!);
        }).AddHttpMessageHandler<JwtForwardingHandler>();

      
        services.AddHttpClient<IInventoryServiceClient,InventoryServiceClient>(client =>
                {
                    client.BaseAddress =
                        new Uri(
                            configuration["Services:InventoryService"]!);
                }).AddHttpMessageHandler<JwtForwardingHandler>();
        services.AddHttpClient<IPaymentServiceClient,PaymentServiceClient>(client =>
                {
                    client.BaseAddress =
                        new Uri(
                            configuration["Services:PaymentService"]!);
                }).AddHttpMessageHandler<JwtForwardingHandler>();

        return services;
    }
}