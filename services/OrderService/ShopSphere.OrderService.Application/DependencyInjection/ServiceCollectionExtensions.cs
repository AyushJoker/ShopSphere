
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.OrderService.Application.Interfaces;
using ShopSphere.OrderService.Application.Validators;

namespace ShopSphere.OrderService.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IOrderService,Services.OrderService>();

        services.AddFluentValidation();

        services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();

        return services;
    }
}