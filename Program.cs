using PortfolioManager.Api.Configuration;
using PortfolioManager.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddLoggingConfig();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioningConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddCorsConfig();
builder.Services.AddRateLimiterConfig();
builder.Services.AddHealthCheckConfig(builder.Configuration);
builder.Services.AddDependencyInjectionConfig();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapHealthChecks("/health");

app.UseRateLimiter();

app.Run();