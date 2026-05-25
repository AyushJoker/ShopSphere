using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace ShopSphere.IdentityService.API.Configurations
{
    public static class RateLimitingConfiguration
    {
        public static IServiceCollection AddRateLimitingConfiguration(
            this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(
                    policyName: "login-policy",
                    limiterOptions =>
                    {
                        limiterOptions.PermitLimit = 5;

                        limiterOptions.Window =
                            TimeSpan.FromMinutes(1);

                        limiterOptions.QueueProcessingOrder =
                            QueueProcessingOrder.OldestFirst;

                        limiterOptions.QueueLimit = 0;
                    });

                options.RejectionStatusCode = 429;
            });

            return services;
        }
    }
}