using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.ProductService.Application.Interfaces;
using ShopSphere.ProductService.Application.Validators;

namespace ShopSphere.ProductService.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IProductService,
            Services.ProductService>();

        services.AddFluentValidation();

        services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

        return services;
    }
}