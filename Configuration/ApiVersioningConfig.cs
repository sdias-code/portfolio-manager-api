using Asp.Versioning;

namespace PortfolioManager.Api.Configuration;

public static class ApiVersioningConfig
{
    public static void AddApiVersioningConfig(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
    }
}