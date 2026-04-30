using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Exceptions;
using ShopComponents.Core.Interfaces;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Services.Services;

public class FacturaService : IFacturaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FacturaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FacturaDto>> GetAllAsync()
    {
        var facturas = await _unitOfWork.Facturas.GetAllAsync();
        return _mapper.Map<IEnumerable<FacturaDto>>(facturas);
    }

    public async Task<FacturaDto?> GetByIdAsync(int id)
    {
        var factura = await _unitOfWork.Facturas.GetByIdAsync(id);
        return factura is null ? null : _mapper.Map<FacturaDto>(factura);
    }

    public async Task<FacturaDto> CreateAsync(FacturaDto dto)
    {
        var venta = await _unitOfWork.Ventas.GetByIdAsync(dto.VentaId);
        if (venta is null)
            throw new BusinessException("No se puede emitir factura sin una venta existente.", 400);

        var entity = _mapper.Map<Factura>(dto);

        await _unitOfWork.Facturas.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<FacturaDto>(entity);
    }

    public async Task<FacturaDto> UpdateAsync(int id, FacturaDto dto)
    {
        var entity = await _unitOfWork.Facturas.GetByIdAsync(id);
        if (entity is null)
            throw new BusinessException("La factura no existe.", 404);

        _mapper.Map(dto, entity);

        _unitOfWork.Facturas.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<FacturaDto>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Facturas.GetByIdAsync(id);
        if (entity is null)
            throw new BusinessException("La factura no existe.", 404);

        _unitOfWork.Facturas.Delete(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}