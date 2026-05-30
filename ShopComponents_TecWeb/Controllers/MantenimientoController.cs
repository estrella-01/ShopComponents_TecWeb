using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.CustomEntities;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Enum;          
using ShopComponents.Core.QueryFilters;
using ShopComponents.Services.Interfaces;
using System.Net;

namespace ShopComponents.Api.Controllers;

/// <summary>
/// Gestión de servicios de mantenimiento de componentes
/// </summary>
[Authorize]
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class MantenimientoController : ControllerBase
{
    private readonly IMantenimientoService _mantenimientoService;

    public MantenimientoController(IMantenimientoService mantenimientoService)
    {
        _mantenimientoService = mantenimientoService;
    }

    /// <summary>Obtiene lista paginada de servicios de mantenimiento</summary>
    /// <param name="filter">Filtros opcionales: ClienteId, Estado, FechaDesde, FechaHasta, Descripcion, Page, PageSize</param>
    /// <response code="200">Lista de mantenimientos</response>
    /// <response code="401">No autorizado</response>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetAll([FromQuery] MantenimientoFilter? filter)
    {
        var data = await _mantenimientoService.GetAllAsync(filter);
        var paged = PagedList<MantenimientoDto>.Create(data, filter?.Page ?? 1, filter?.PageSize ?? 10);

        var pagination = new Pagination
        {
            TotalCount = paged.TotalCount,
            PageSize = paged.PageSize,
            CurrentPage = paged.CurrentPage,
            TotalPages = paged.TotalPages,
            HasNextPage = paged.HasNextPage,
            HasPreviousPage = paged.HasPreviousPage
        };

        var messages = paged.Any()
            ? new[] { new Message { Type = TypeMessage.success.ToString(), Description = "Servicios de mantenimiento recuperados correctamente." } }
            : new[] { new Message { Type = TypeMessage.warning.ToString(), Description = "No se encontraron servicios de mantenimiento." } };

        var response = new ApiResponse<IEnumerable<MantenimientoDto>>(paged)
        {
            Pagination = pagination,
            Messages = messages
        };

        return Ok(response);
    }

    /// <summary>Obtiene un servicio de mantenimiento por su ID</summary>
    /// <param name="id">Identificador del mantenimiento</param>
    /// <response code="200">Mantenimiento encontrado</response>
    /// <response code="404">No encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetById(int id)
    {
        var mantenimiento = await _mantenimientoService.GetByIdAsync(id);
        if (mantenimiento is null)
            return NotFound(new { Status = 404, Message = "Servicio de mantenimiento no encontrado." });

        var response = new ApiResponse<MantenimientoDto>(mantenimiento)
        {
            Messages = new[] { new Message { Type = TypeMessage.success.ToString(), Description = "Mantenimiento encontrado." } }
        };
        return Ok(response);
    }

    /// <summary>Registra un nuevo servicio de mantenimiento</summary>
    /// <param name="dto">Estado permitido: Pendiente | En proceso | Completado | Cancelado</param>
    /// <response code="201">Creado correctamente</response>
    /// <response code="400">Datos inválidos</response>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Create([FromBody] MantenimientoDto dto)
    {
        var created = await _mantenimientoService.CreateAsync(dto);
        var response = new ApiResponse<MantenimientoDto>(created)
        {
            Messages = new[] { new Message { Type = TypeMessage.success.ToString(), Description = "Servicio de mantenimiento registrado correctamente." } }
        };
        return StatusCode(201, response);
    }

    /// <summary>Actualiza un servicio de mantenimiento existente</summary>
    /// <param name="id">ID del mantenimiento</param>
    /// <param name="dto">Datos actualizados</param>
    /// <response code="200">Actualizado</response>
    /// <response code="404">No encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Update(int id, [FromBody] MantenimientoDto dto)
    {
        var updated = await _mantenimientoService.UpdateAsync(id, dto);
        var response = new ApiResponse<MantenimientoDto>(updated)
        {
            Messages = new[] { new Message { Type = TypeMessage.success.ToString(), Description = "Servicio de mantenimiento actualizado correctamente." } }
        };
        return Ok(response);
    }

    /// <summary>Elimina un servicio de mantenimiento</summary>
    /// <param name="id">ID del mantenimiento</param>
    /// <response code="204">Eliminado</response>
    /// <response code="404">No encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Delete(int id)
    {
        await _mantenimientoService.DeleteAsync(id);
        return NoContent();
    }
}