using ShopComponents.Core.Interfaces;
using System.Data;

namespace ShopComponents.Infraestructure.Data;

public class DapperContext : IDapperContext
{
    private readonly IDbConnectionFactory _factory;

    public DapperContext(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public IDbConnection CreateConnection() => _factory.CreateConnection();
}