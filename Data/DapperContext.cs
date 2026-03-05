using System.Data;
using Npgsql;

namespace PortfolioManager.Api.Data;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection");

        Console.WriteLine($"CONNECTION: {conn}");

        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not configured.");
    }

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
}