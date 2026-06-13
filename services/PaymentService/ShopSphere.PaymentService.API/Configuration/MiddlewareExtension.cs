using ShopSphere.PaymentService.API.Middleware;

namespace ShopSphere.PaymentService.API.Configuration;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}