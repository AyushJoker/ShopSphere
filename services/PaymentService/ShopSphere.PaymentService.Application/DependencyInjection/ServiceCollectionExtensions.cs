using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.PaymentService.Application.Interfaces;


namespace ShopSphere.PaymentService.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, Services.PaymentService>();
       
        services.AddFluentValidationAutoValidation();

       // services.AddFluentValidationAutoValidation();

       services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        return services;
    }
}