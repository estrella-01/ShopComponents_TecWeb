using ShopComponents.Core.Entities;
using ShopComponents.Core.QueryFilters;

namespace ShopComponents.Core.Interfaces;

public interface IVentaRepository
{
    Task<IEnumerable<Ventum>> GetAllAsync(VentaFilter? filter = null);
    Task<Ventum?> GetByIdAsync(int id);
    Task AddAsync(Ventum entity);
    void Update(Ventum entity);
    void Delete(Ventum entity);
}