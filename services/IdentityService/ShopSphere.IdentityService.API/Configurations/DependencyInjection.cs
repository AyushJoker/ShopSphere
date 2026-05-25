using Microsoft.EntityFrameworkCore;
using ShopSphere.IdentityService.Application.Interfaces;
using ShopSphere.IdentityService.Application.Services;
using ShopSphere.IdentityService.Infrastructure.Data;
using ShopSphere.IdentityService.Infrastructure.Repositories;
using ShopSphere.IdentityService.Infrastructure.Security;

namespace ShopSphere.IdentityService.API.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString(
                    "DefaultConnection"));
        });

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IJwtTokenGenerator,
            JwtTokenGenerator>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IRefreshTokenGenerator,
            RefreshTokenGenerator>();

        services.AddScoped<IRefreshTokenRepository,
            RefreshTokenRepository>();

        return services;
    }
}