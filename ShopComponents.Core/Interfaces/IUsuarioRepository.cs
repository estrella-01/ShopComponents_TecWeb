using ShopComponents.Core.Entities;

namespace ShopComponents.Core.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id);
}