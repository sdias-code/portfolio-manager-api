namespace PortfolioManager.Api.Configuration;

public static class HealthCheckConfig
{
    public static void AddHealthCheckConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("DefaultConnection"));
    }
}