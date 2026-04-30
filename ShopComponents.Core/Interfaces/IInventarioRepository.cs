using ShopComponents.Core.Entities;

namespace ShopComponents.Core.Interfaces;

public interface IInventarioRepository
{
    Task<IEnumerable<Inventario>> GetAllAsync();
    Task<IEnumerable<Inventario>> GetByProductoIdAsync(int productoId);
    Task InsertAsync(Inventario inventario);
}