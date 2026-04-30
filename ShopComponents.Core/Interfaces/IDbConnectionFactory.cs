using System.Data;

namespace ShopComponents.Core.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}