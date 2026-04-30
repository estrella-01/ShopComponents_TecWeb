using System.Data;

namespace ShopComponents.Core.Interfaces;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}