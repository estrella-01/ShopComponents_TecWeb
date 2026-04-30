using Microsoft.Extensions.Configuration;
using MySqlConnector;
using ShopComponents.Core.Interfaces;
using System.Data;

namespace ShopComponents.Infraestructure.Repositories;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        var provider = _configuration["DatabaseProvider"] ?? "MySql";
        var connectionString = _configuration.GetConnectionString("DefaultConnection")!;

        return new MySqlConnection(connectionString);
    }
}