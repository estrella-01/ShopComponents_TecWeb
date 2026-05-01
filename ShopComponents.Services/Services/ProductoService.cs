using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Exceptions;
using ShopComponents.Core.Interfaces;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Services.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repository;
    private readonly IMapper _mapper;

    public ProductoService(IProductoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Producto> GetByIdAsync(int id)
    {
        var producto = await _repository.GetByIdAsync(id);
        if (producto is null)
            throw new BusinessException("Producto no encontrado.", 404);
        return producto;
    }

    public async Task InsertAsync(Producto producto)
        => await _repository.InsertAsync(producto);

    public async Task UpdateAsync(Producto producto)
        => await _repository.UpdateAsync(producto);

    public async Task DeleteAsync(int id)
        => await _repository.DeleteAsync(id);
}