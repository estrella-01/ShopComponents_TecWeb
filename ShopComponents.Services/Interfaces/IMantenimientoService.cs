using ShopComponents.Core.DTOs;
using ShopComponents.Core.QueryFilters;

namespace ShopComponents.Services.Interfaces;

public interface IMantenimientoService
{
    Task<IEnumerable<MantenimientoDto>> GetAllAsync(MantenimientoFilter? filter = null);
    Task<MantenimientoDto?> GetByIdAsync(int id);
    Task<MantenimientoDto> CreateAsync(MantenimientoDto dto);
    Task<MantenimientoDto> UpdateAsync(int id, MantenimientoDto dto);
    Task DeleteAsync(int id);
}