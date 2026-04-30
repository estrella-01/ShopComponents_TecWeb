using ShopComponents.Core.Entities;

namespace ShopComponents.Core.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(int id);
}   