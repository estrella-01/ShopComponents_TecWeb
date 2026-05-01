using ShopComponents.Core.DTOs;
using ShopComponents.Core.QueryFilters;

namespace ShopComponents.Services.Interfaces;

public interface IInventarioService
{
    Task<IEnumerable<InventarioDto>> GetAllAsync(InventarioFilter? filter = null);
    Task<IEnumerable<InventarioDto>> GetByProductoIdAsync(int productoId);
    Task RegistrarMovimientoAsync(InventarioDto dto);
}