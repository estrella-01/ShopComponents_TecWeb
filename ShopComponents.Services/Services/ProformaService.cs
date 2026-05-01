using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Exceptions;
using ShopComponents.Core.Interfaces;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Services.Services;

public class ProformaService : IProformaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProformaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProformaDto>> GetAllAsync()
    {
        var proformas = await _unitOfWork.Proformas.GetAllAsync();
        return _mapper.Map<IEnumerable<ProformaDto>>(proformas);
    }

    public async Task<ProformaDto?> GetByIdAsync(int id)
    {
        var proforma = await _unitOfWork.Proformas.GetByIdAsync(id);
        return proforma is null ? null : _mapper.Map<ProformaDto>(proforma);
    }

    public async Task<ProformaDto> CreateAsync(ProformaDto dto)
    {
        if (dto.Detalles == null || !dto.Detalles.Any())
            throw new BusinessException("La proforma debe tener al menos un producto.", 400);

        var entity = _mapper.Map<Proforma>(dto);
        entity.Fecha = DateTime.Now;
        entity.Total = dto.Detalles.Sum(d => d.Cantidad * d.Precio);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _unitOfWork.Proformas.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ProformaDto>(entity);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var proforma = await _unitOfWork.Proformas.GetByIdAsync(id);
        if (proforma is null)
            throw new BusinessException("Proforma no encontrada.", 404);

        _unitOfWork.Proformas.Delete(proforma);
        await _unitOfWork.SaveChangesAsync();
    }
}