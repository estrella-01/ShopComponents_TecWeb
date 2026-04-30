using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infraestructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SistemaDbContext _context;

    public UsuarioRepository(SistemaDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
    }
}