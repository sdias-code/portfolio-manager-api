using Microsoft.OpenApi.Models;
using PortfolioManager.Api.Application.Interfaces;
using PortfolioManager.Api.Data;
using PortfolioManager.Api.Infrastructure.Interfaces;
using PortfolioManager.Api.Infrastructure.Repositories;
using PortfolioManager.Api.Infrastructure.Swagger;
using PortfolioManager.Api.Middleware;
using Serilog;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// LOGGER
builder.Host.UseSerilog((context, config) =>
{
    config
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
});

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var test = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"ConnectionString: {test}");

// DAPPER
builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<IDbConnection>(sp =>
    sp.GetRequiredService<DapperContext>().CreateConnection()
);

// DEPENDENCY INJECTION
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

var app = builder.Build();

// MIDDLEWARES
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();