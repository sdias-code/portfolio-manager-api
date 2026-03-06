using PortfolioManager.Api.Application.Interfaces;
using PortfolioManager.Api.Data;
using PortfolioManager.Api.Infrastructure.Interfaces;
using PortfolioManager.Api.Infrastructure.Repositories;
using System.Data;

namespace PortfolioManager.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfig(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        services.AddScoped<IDbConnection>(sp =>
            sp.GetRequiredService<DapperContext>().CreateConnection()
        );

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectService, ProjectService>();
    }
}