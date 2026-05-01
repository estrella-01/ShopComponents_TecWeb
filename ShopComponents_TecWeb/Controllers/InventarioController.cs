using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.CustomEntities;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.QueryFilters;
using ShopComponents.Services.Interfaces;
using ShopComponents.Services.Validators;

namespace ShopComponents.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventarioController : ControllerBase
{
    private readonly IInventarioService _inventarioService;
    private readonly InventarioDtoValidator _validator;

    public InventarioController(IInventarioService inventarioService, InventarioDtoValidator validator)
    {
        _inventarioService = inventarioService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] InventarioFilter? filter)
    {
        var data = await _inventarioService.GetAllAsync(filter);
        var paged = PagedList<InventarioDto>.Create(data, filter?.Page ?? 1, filter?.PageSize ?? 10);

        var response = new ApiResponse<IEnumerable<InventarioDto>>(paged)
        {
            Pagination = new Pagination
            {
                TotalCount = paged.TotalCount,
                PageSize = paged.PageSize,
                CurrentPage = paged.CurrentPage,
                TotalPages = paged.TotalPages,
                HasNextPage = paged.HasNextPage,
                HasPreviousPage = paged.HasPreviousPage
            }
        };

        return Ok(response);
    }

    [HttpGet("producto/{productoId}")]
    public async Task<IActionResult> GetByProducto(int productoId)
    {
        var data = await _inventarioService.GetByProductoIdAsync(productoId);
        return Ok(new ApiResponse<IEnumerable<InventarioDto>>(data));
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarMovimiento([FromBody] InventarioDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        await _inventarioService.RegistrarMovimientoAsync(dto);
        return Ok(new ApiResponse<string>("Movimiento registrado correctamente."));
    }
}