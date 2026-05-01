using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infraestructure.Repositories;

public class ProformaRepository : IProformaRepository
{
    private readonly SistemaDbContext _context;

    public ProformaRepository(SistemaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Proforma>> GetAllAsync()
        => await _context.Proformas.Include(p => p.Cliente).ToListAsync();

    public async Task<Proforma?> GetByIdAsync(int id)
        => await _context.Proformas.Include(p => p.Cliente).FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Proforma proforma)
        => await _context.Proformas.AddAsync(proforma);

    public void Delete(Proforma proforma)
        => _context.Proformas.Remove(proforma);
}