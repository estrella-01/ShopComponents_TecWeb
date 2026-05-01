using ShopComponents.Core.DTOs;

namespace ShopComponents.Services.Interfaces;

public interface IProformaService
{
    Task<IEnumerable<ProformaDto>> GetAllAsync();
    Task<ProformaDto?> GetByIdAsync(int id);
    Task<ProformaDto> CreateAsync(ProformaDto dto);
    Task DeleteAsync(int id);
}