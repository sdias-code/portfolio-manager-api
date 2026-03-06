using Microsoft.OpenApi.Models;
using PortfolioManager.Api.Infrastructure.Swagger;

namespace PortfolioManager.Api.Configuration;

public static class SwaggerConfig
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Portfolio Manager API",
                Version = "v1",
                Description = "API para gerenciamento de projetos do portfólio",
                Contact = new OpenApiContact
                {
                    Name = "Silvio Ferreira",
                    Email = "silviodias.ms@gmail.com"
                }
            });

            options.OperationFilter<RemoveVersionFromParameter>();
            options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
        });
    }
}