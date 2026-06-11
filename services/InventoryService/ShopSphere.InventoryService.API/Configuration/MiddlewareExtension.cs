using ShopSphere.InventoryService.API.Middleware;

namespace ShopSphere.InventoryService.API.Configuration;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}