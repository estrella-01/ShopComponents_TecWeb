using ShopComponents.Core.Entities;
using ShopComponents.Infraestructure.Data;

public class ProformaRepository : IProformaRepository
{
    private readonly SistemaDbContext _context;

    public ProformaRepository(SistemaDbContext context)
    {
        _context = context;
    }

    public async Task InsertAsync(Proforma proforma)
    {
        _context.Proformas.Add(proforma);
        await _context.SaveChangesAsync();
    }
}