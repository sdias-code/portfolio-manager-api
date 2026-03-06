using Serilog;

namespace PortfolioManager.Api.Configuration;

public static class LoggingConfig
{
    public static void AddLoggingConfig(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, config) =>
        {
            config
                .ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
        });
    }
}
