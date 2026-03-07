namespace PortfolioManager.Api.Configuration
{
    public static class CorsConfig
    {
        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .WithOrigins(
                            "http://localhost:5173",                        // Frontend local
                            "https://portfolio-manager-api-jpfh.onrender.com" // Frontend deploy
                        )
                        .AllowAnyHeader()   // Headers permitidos (Content-Type, Authorization, etc.)
                        .AllowAnyMethod()   // Métodos permitidos (GET, POST, PUT, DELETE, PATCH)
                        .AllowCredentials(); // Permite envio de cookies/autenticação se necessário
                });
            });
        }
    }
}