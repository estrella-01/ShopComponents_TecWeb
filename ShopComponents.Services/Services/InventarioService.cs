using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Exceptions;
using ShopComponents.Core.Interfaces;
using ShopComponents.Core.QueryFilters;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Services.Services;

public class InventarioService : IInventarioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InventarioService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventarioDto>> GetAllAsync(InventarioFilter? filter = null)
    {
        var data = await _unitOfWork.Inventarios.GetAllAsync(filter);
        return _mapper.Map<IEnumerable<InventarioDto>>(data);
    }

    public async Task<IEnumerable<InventarioDto>> GetByProductoIdAsync(int productoId)
    {
        var data = await _unitOfWork.Inventarios.GetByProductoIdAsync(productoId);
        return _mapper.Map<IEnumerable<InventarioDto>>(data);
    }

    public async Task RegistrarMovimientoAsync(InventarioDto dto)
    {
        var movimientos = await _unitOfWork.Inventarios.GetByProductoIdAsync(dto.ProductoId);
        if (!movimientos.Any() && dto.TipoMovimiento == "salida")
            throw new BusinessException("No hay stock registrado para este producto.", 400);

        var stockActual = movimientos
            .Sum(i => i.TipoMovimiento == "entrada" ? i.Cantidad : -i.Cantidad);

        if (dto.TipoMovimiento == "salida" && stockActual < dto.Cantidad)
            throw new BusinessException($"Stock insuficiente. Stock actual: {stockActual}.", 400);

        if (dto.TipoMovimiento != "entrada" && dto.TipoMovimiento != "salida")
            throw new BusinessException("TipoMovimiento debe ser 'entrada' o 'salida'.", 400);

        var inventario = new Inventario
        {
            ProductoId = dto.ProductoId,
            TipoMovimiento = dto.TipoMovimiento,
            Cantidad = dto.Cantidad,
            Fecha = DateTime.Now
        };

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _unitOfWork.Inventarios.InsertAsync(inventario);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}