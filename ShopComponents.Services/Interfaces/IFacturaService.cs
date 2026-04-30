using ShopComponents.Core.DTOs;

namespace ShopComponents.Services.Interfaces;

public interface IFacturaService
{
    Task<IEnumerable<FacturaDto>> GetAllAsync();
    Task<FacturaDto?> GetByIdAsync(int id);
    Task<FacturaDto> CreateAsync(FacturaDto dto);
    Task<FacturaDto> UpdateAsync(int id, FacturaDto dto);
    Task DeleteAsync(int id);
}