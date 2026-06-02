using Serilog;

namespace ShopSphere.ProductService.API.Configurations;

public static class LoggingConfiguration
{
    public static WebApplicationBuilder AddSerilogLogging(
     this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }
}