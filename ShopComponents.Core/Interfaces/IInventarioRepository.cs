using ShopComponents.Core.Entities;
using ShopComponents.Core.QueryFilters;

namespace ShopComponents.Core.Interfaces;

public interface IInventarioRepository
{
    Task<IEnumerable<Inventario>> GetAllAsync(InventarioFilter? filter = null);
    Task<IEnumerable<Inventario>> GetByProductoIdAsync(int productoId);
    Task InsertAsync(Inventario inventario);
}