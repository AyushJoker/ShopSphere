using ShopSphere.ProductService.API.Middleware;

namespace ShopSphere.ProductService.API.Configuration
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}
