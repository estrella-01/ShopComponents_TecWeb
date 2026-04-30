using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Services.Services;

public class InventarioService : IInventarioService
{
    private readonly IInventarioRepository _inventarioRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IMapper _mapper;

    public InventarioService(
        IInventarioRepository inventarioRepository,
        IProductoRepository productoRepository,
        IMapper mapper)
    {
        _inventarioRepository = inventarioRepository;
        _productoRepository = productoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventarioDto>> GetAllAsync()
    {
        var data = await _inventarioRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<InventarioDto>>(data);
    }

    public async Task<IEnumerable<InventarioDto>> GetByProductoIdAsync(int productoId)
    {
        var data = await _inventarioRepository.GetByProductoIdAsync(productoId);
        return _mapper.Map<IEnumerable<InventarioDto>>(data);
    }

    public async Task RegistrarMovimientoAsync(InventarioDto dto)
    {
        var producto = await _productoRepository.GetByIdAsync(dto.ProductoId);

        if (producto == null)
            throw new Exception("Producto no encontrado");

        if (dto.TipoMovimiento == "salida" && producto.Stock < dto.Cantidad)
            throw new Exception("Stock insuficiente para realizar la salida");

        // Actualizar stock
        if (dto.TipoMovimiento == "entrada")
            producto.Stock += dto.Cantidad;
        else if (dto.TipoMovimiento == "salida")
            producto.Stock -= dto.Cantidad;
        else
            throw new Exception("TipoMovimiento debe ser 'entrada' o 'salida'");

        await _productoRepository.UpdateAsync(producto);

        var inventario = new Inventario
        {
            ProductoId = dto.ProductoId,
            TipoMovimiento = dto.TipoMovimiento,
            Cantidad = dto.Cantidad,
            Fecha = DateTime.Now
        };

        await _inventarioRepository.InsertAsync(inventario);
    }
}