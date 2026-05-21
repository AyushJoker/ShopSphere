using ShopSphere.IdentityService.API.Middleware;

namespace ShopSphere.IdentityService.API.Configurations;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder
        UseGlobalExceptionMiddleware(
            this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}