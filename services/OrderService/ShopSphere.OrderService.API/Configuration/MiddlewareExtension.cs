using ShopSphere.OrderService.API.Middleware;

namespace ShopSphere.OrderService.API.Configuration
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
