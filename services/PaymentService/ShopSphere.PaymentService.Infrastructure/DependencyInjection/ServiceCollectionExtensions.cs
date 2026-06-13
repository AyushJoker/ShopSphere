using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.PaymentService.Infrastructure.Repositories;
using ShopSphere.PaymentService.Application.Interfaces;
using ShopSphere.PaymentService.Infrastructure.Data;


namespace ShopSphere.PaymentService.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PaymentDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString(
                    "DefaultConnection")));



        services.AddScoped<IPaymentRepository,PaymentRepository>();

        services.AddJwtAuthentication(configuration);

        return services;
    }
}