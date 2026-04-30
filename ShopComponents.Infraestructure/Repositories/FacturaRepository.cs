using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infraestructure.Repositories;

public class FacturaRepository : BaseRepository<Factura>, IFacturaRepository
{
    public FacturaRepository(SistemaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Factura>> GetAllAsync()
        => await _context.Facturas.ToListAsync();

    public async Task<Factura?> GetByIdAsync(int id)
        => await _context.Facturas.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(Factura entity)
        => await _context.Facturas.AddAsync(entity);

    public void Update(Factura entity) => _context.Facturas.Update(entity);
    public void Delete(Factura entity) => _context.Facturas.Remove(entity);
}