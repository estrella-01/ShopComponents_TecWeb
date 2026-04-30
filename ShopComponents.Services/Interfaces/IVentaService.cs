using ShopComponents.Core.DTOs;
using ShopComponents.Core.QueryFilters;

namespace ShopComponents.Services.Interfaces;

public interface IVentaService
{
    Task<IEnumerable<VentaDto>> GetAllAsync(VentaFilter? filter = null);
    Task<VentaDto?> GetByIdAsync(int id);
    Task<VentaDto> CreateAsync(VentaDto dto);
    Task<VentaDto> UpdateAsync(int id, VentaDto dto);
    Task DeleteAsync(int id);
}