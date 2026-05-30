using ShopComponents.Core.Entities;
using ShopComponents.Core.QueryFilters;

namespace ShopComponents.Core.Interfaces;

public interface IMantenimientoRepository
{
    Task<IEnumerable<Mantenimiento>> GetAllAsync(MantenimientoFilter? filter = null);
    Task<Mantenimiento?> GetByIdAsync(int id);
    Task AddAsync(Mantenimiento mantenimiento);
    void Update(Mantenimiento mantenimiento);
    void Delete(Mantenimiento mantenimiento);
    Task SaveChangesAsync();
}