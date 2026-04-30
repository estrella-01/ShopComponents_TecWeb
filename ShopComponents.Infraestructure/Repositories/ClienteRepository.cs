using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infraestructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly SistemaDbContext _context;

    public ClienteRepository(SistemaDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
    }
}