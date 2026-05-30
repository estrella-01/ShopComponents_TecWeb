using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;

namespace ShopComponents.Services.Interfaces;

public interface IUsuarioService
{
    Task<Usuario?> GetByCredentialsAsync(UserLogin login);
    Task RegisterAsync(UsuarioDto dto);
}