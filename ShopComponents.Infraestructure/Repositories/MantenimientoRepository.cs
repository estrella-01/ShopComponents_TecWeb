using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Core.QueryFilters;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infraestructure.Repositories;

public class MantenimientoRepository : IMantenimientoRepository
{
    private readonly SistemaDbContext _context;

    public MantenimientoRepository(SistemaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Mantenimiento>> GetAllAsync(MantenimientoFilter? filter = null)
    {
        var query = _context.Mantenimientos.Include(m => m.Cliente).AsQueryable();

        if (filter is not null)
        {
            if (filter.ClienteId.HasValue)
                query = query.Where(m => m.ClienteId == filter.ClienteId.Value);

            if (!string.IsNullOrEmpty(filter.Estado))
                query = query.Where(m => m.Estado == filter.Estado);

            if (!string.IsNullOrEmpty(filter.Descripcion))
                query = query.Where(m => m.Descripcion.ToLower().Contains(filter.Descripcion.ToLower()));

            if (filter.FechaDesde.HasValue)
                query = query.Where(m => m.Fecha >= filter.FechaDesde.Value);

            if (filter.FechaHasta.HasValue)
                query = query.Where(m => m.Fecha <= filter.FechaHasta.Value);
        }

        return await query.OrderByDescending(m => m.Fecha).ToListAsync();
    }

    public async Task<Mantenimiento?> GetByIdAsync(int id)
    {
        return await _context.Mantenimientos
            .Include(m => m.Cliente)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(Mantenimiento mantenimiento)
    {
        await _context.Mantenimientos.AddAsync(mantenimiento);
    }

    public void Update(Mantenimiento mantenimiento)
    {
        _context.Mantenimientos.Update(mantenimiento);
    }

    public void Delete(Mantenimiento mantenimiento)
    {
        _context.Mantenimientos.Remove(mantenimiento);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}