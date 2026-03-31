using ShopComponents.Core.Entities;

namespace ShopComponents.Services.Interfaces;

public interface IProductoService
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task InsertAsync(Producto producto);
    Task<Producto> GetByIdAsync(int id);
    Task UpdateAsync(Producto producto);
    Task DeleteAsync(int id);
    Task<object> CrearProforma(int productoId, int cantidad);
}