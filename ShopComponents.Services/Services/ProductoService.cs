using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Services.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repository;

    public ProductoService(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task InsertAsync(Producto producto)
    {
        await _repository.InsertAsync(producto);
    }
}