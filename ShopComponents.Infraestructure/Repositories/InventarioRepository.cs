using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infrastructure.Repositories;

public class InventarioRepository : IInventarioRepository
{
    private readonly SistemaDbContext _context;

    public InventarioRepository(SistemaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inventario>> GetAllAsync()
    {
        return await _context.Inventarios.Include(i => i.Producto).ToListAsync();
    }

    public async Task<IEnumerable<Inventario>> GetByProductoIdAsync(int productoId)
    {
        return await _context.Inventarios
            .Where(i => i.ProductoId == productoId)
            .Include(i => i.Producto)
            .ToListAsync();
    }

    public async Task InsertAsync(Inventario inventario)
    {
        _context.Inventarios.Add(inventario);
        await _context.SaveChangesAsync();
    }
}