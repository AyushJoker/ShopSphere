using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.InventoryService.Application.Interfaces;
using ShopSphere.InventoryService.Application.Validators;

namespace ShopSphere.InventoryService.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IInventoryService,Services.InventoryService>();
       
       services.AddFluentValidationAutoValidation();

        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        return services;
    }
}