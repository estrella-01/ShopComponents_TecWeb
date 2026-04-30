using ShopComponents.Core.Entities;

namespace ShopComponents.Core.Interfaces;

public interface IFacturaRepository
{
    Task<IEnumerable<Factura>> GetAllAsync();
    Task<Factura?> GetByIdAsync(int id);
    Task AddAsync(Factura entity);
    void Update(Factura entity);
    void Delete(Factura entity);
}