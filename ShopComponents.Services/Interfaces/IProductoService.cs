using ShopComponents.Core.Entities;

namespace ShopComponents.Services.Interfaces;

public interface IProductoService
{
    Task<IEnumerable<Producto>> GetAllAsync();
}