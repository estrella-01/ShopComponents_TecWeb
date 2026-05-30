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

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    // ← FALTABA ESTE MÉTODO - UsuarioService lo llama al registrar
    public async Task AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }
}
