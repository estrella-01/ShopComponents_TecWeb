using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infrastructure.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly SistemaDbContext _context;

    public ProductoRepository(SistemaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos.ToListAsync();
    }

    public async Task<Producto> GetByIdAsync(int id)
    {
        return await _context.Productos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
    }
}