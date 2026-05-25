using Serilog;

namespace ShopSphere.IdentityService.API.Configurations;

public static class LoggingConfiguration
{
    public static WebApplicationBuilder AddSerilogLogging(
        this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build())
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }
}