using Microsoft.AspNetCore.RateLimiting;

namespace PortfolioManager.Api.Configuration;

public static class RateLimiterConfig
{
    public static void AddRateLimiterConfig(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("fixed", opt =>
            {
                opt.PermitLimit = 50;
                opt.Window = TimeSpan.FromMinutes(5);
                opt.QueueLimit = 0;
            });
        });
    }
}