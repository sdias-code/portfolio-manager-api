using PortfolioManager.Api.Configuration;
using PortfolioManager.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Logger
builder.AddLoggingConfig();

// Controllers e API Explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurações
builder.Services.AddApiVersioningConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddCorsConfig(); // CORS para frontend local + deploy
builder.Services.AddRateLimiterConfig();
builder.Services.AddHealthCheckConfig(builder.Configuration);
builder.Services.AddDependencyInjectionConfig();

var app = builder.Build();

// Middleware de exceções
app.UseMiddleware<ExceptionMiddleware>();

// Redirecionamento HTTPS (somente em dev)
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// CORS (antes de controllers e authorization)
app.UseCors("AllowFrontend");

// Autorização
app.UseAuthorization();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Map Controllers e Health Checks
app.MapControllers();
app.MapHealthChecks("/health");

// Rate Limiter
app.UseRateLimiter();

// Porta para Render (ou fallback)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
