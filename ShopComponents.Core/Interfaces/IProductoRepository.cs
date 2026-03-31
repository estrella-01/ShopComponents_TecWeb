using ShopComponents.Core.Entities;

namespace ShopComponents.Core.Interfaces;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto> GetByIdAsync(int id);
    Task InsertAsync(Producto producto);
}