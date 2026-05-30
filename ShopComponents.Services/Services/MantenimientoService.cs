using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Exceptions;
using ShopComponents.Core.Interfaces;
using ShopComponents.Core.QueryFilters;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Services.Services;

public class MantenimientoService : IMantenimientoService
{
    private readonly IMantenimientoRepository _mantenimientoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public MantenimientoService(
        IMantenimientoRepository mantenimientoRepository,
        IClienteRepository clienteRepository,
        IMapper mapper)
    {
        _mantenimientoRepository = mantenimientoRepository;
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MantenimientoDto>> GetAllAsync(MantenimientoFilter? filter = null)
    {
        var mantenimientos = await _mantenimientoRepository.GetAllAsync(filter);
        return _mapper.Map<IEnumerable<MantenimientoDto>>(mantenimientos);
    }

    public async Task<MantenimientoDto?> GetByIdAsync(int id)
    {
        var mantenimiento = await _mantenimientoRepository.GetByIdAsync(id);
        return mantenimiento is null ? null : _mapper.Map<MantenimientoDto>(mantenimiento);
    }

    public async Task<MantenimientoDto> CreateAsync(MantenimientoDto dto)
    {
        var cliente = await _clienteRepository.GetByIdAsync(dto.ClienteId);
        if (cliente is null)
            throw new BusinessException("El cliente no existe.", 404);

        var estadosPermitidos = new[] { "Pendiente", "En proceso", "Completado", "Cancelado" };
        if (!estadosPermitidos.Contains(dto.Estado))
            throw new BusinessException($"Estado inválido. Valores permitidos: {string.Join(", ", estadosPermitidos)}");

        var entity = _mapper.Map<Mantenimiento>(dto);
        await _mantenimientoRepository.AddAsync(entity);
        await _mantenimientoRepository.SaveChangesAsync();

        return _mapper.Map<MantenimientoDto>(entity);
    }

    public async Task<MantenimientoDto> UpdateAsync(int id, MantenimientoDto dto)
    {
        var entity = await _mantenimientoRepository.GetByIdAsync(id);
        if (entity is null)
            throw new BusinessException("El servicio de mantenimiento no existe.", 404);

        _mapper.Map(dto, entity);
        _mantenimientoRepository.Update(entity);
        await _mantenimientoRepository.SaveChangesAsync();

        return _mapper.Map<MantenimientoDto>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _mantenimientoRepository.GetByIdAsync(id);
        if (entity is null)
            throw new BusinessException("El servicio de mantenimiento no existe.", 404);

        _mantenimientoRepository.Delete(entity);
        await _mantenimientoRepository.SaveChangesAsync();
    }
}