namespace PortfolioManager.Api.Configuration;

public static class HealthCheckConfig
{
    public static void AddHealthCheckConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new System.InvalidOperationException(
                "Connection string 'DefaultConnection' is not configured. " +
                "Please add it to configuration (e.g., appsettings.json or environment variables).");
        }

        services.AddHealthChecks()
            .AddNpgSql(connectionString);
    }
}