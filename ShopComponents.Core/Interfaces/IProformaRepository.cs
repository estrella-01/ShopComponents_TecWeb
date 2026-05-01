using ShopComponents.Core.Entities;

namespace ShopComponents.Core.Interfaces;

public interface IProformaRepository
{
    Task<IEnumerable<Proforma>> GetAllAsync();
    Task<Proforma?> GetByIdAsync(int id);
    Task AddAsync(Proforma proforma);
    void Delete(Proforma proforma);
}