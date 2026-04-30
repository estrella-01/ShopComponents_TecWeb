using ShopComponents.Core.DTOs;

namespace ShopComponents.Services.Interfaces;

public interface IInventarioService
{
    Task<IEnumerable<InventarioDto>> GetAllAsync();
    Task<IEnumerable<InventarioDto>> GetByProductoIdAsync(int productoId);
    Task RegistrarMovimientoAsync(InventarioDto dto);
}