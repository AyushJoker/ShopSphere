using Microsoft.AspNetCore.RateLimiting;

namespace ShopSphere.ApiGateway.Configuration;

    public static class RateLimitingConfiguration
    {
        public static IServiceCollection AddRateLimitingConfiguration(
              this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddFixedWindowLimiter(
                    "GlobalPolicy",
                    limiterOptions =>
                    {
                        limiterOptions.PermitLimit = 100;

                        limiterOptions.Window = TimeSpan.FromMinutes(1);

                        limiterOptions.QueueLimit = 0;
                    });
            });

        return services;
        }
    }

