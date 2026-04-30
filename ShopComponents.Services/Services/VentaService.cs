using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Exceptions;
using ShopComponents.Core.Interfaces;
using ShopComponents.Core.QueryFilters;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Services.Services;

public class VentaService : IVentaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClienteRepository _clienteRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public VentaService(
        IUnitOfWork unitOfWork,
        IClienteRepository clienteRepository,
        IUsuarioRepository usuarioRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _clienteRepository = clienteRepository;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VentaDto>> GetAllAsync(VentaFilter? filter = null)
    {
        var ventas = await _unitOfWork.Ventas.GetAllAsync(filter);
        return _mapper.Map<IEnumerable<VentaDto>>(ventas);
    }

    public async Task<VentaDto?> GetByIdAsync(int id)
    {
        var venta = await _unitOfWork.Ventas.GetByIdAsync(id);
        return venta is null ? null : _mapper.Map<VentaDto>(venta);
    }

    public async Task<VentaDto> CreateAsync(VentaDto dto)
    {
        if (!dto.ClienteId.HasValue || await _clienteRepository.GetByIdAsync(dto.ClienteId.Value) is null)
            throw new BusinessException("El cliente no existe.", 404);

        if (!dto.UsuarioId.HasValue || await _usuarioRepository.GetByIdAsync(dto.UsuarioId.Value) is null)
            throw new BusinessException("El usuario no existe.", 404);

        var entity = _mapper.Map<Ventum>(dto);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _unitOfWork.Ventas.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return _mapper.Map<VentaDto>(entity);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<VentaDto> UpdateAsync(int id, VentaDto dto)
    {
        var entity = await _unitOfWork.Ventas.GetByIdAsync(id);
        if (entity is null)
            throw new BusinessException("La venta no existe.", 404);

        _mapper.Map(dto, entity);

        _unitOfWork.Ventas.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<VentaDto>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Ventas.GetByIdAsync(id);
        if (entity is null)
            throw new BusinessException("La venta no existe.", 404);

        _unitOfWork.Ventas.Delete(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}