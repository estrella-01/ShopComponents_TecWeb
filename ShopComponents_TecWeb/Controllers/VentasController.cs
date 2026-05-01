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
public class VentaController : ControllerBase
{
    private readonly IVentaService _ventaService;
    private readonly VentaDtoValidator _validator;

    public VentaController(IVentaService ventaService, VentaDtoValidator validator)
    {
        _ventaService = ventaService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] VentaFilter? filter)
    {
        var ventas = await _ventaService.GetAllAsync(filter);
        var paged = PagedList<VentaDto>.Create(ventas, filter?.Page ?? 1, filter?.PageSize ?? 10);

        var pagination = new Pagination
        {
            TotalCount = paged.TotalCount,
            PageSize = paged.PageSize,
            CurrentPage = paged.CurrentPage,
            TotalPages = paged.TotalPages,
            HasNextPage = paged.HasNextPage,
            HasPreviousPage = paged.HasPreviousPage
        };

        var response = new ApiResponse<IEnumerable<VentaDto>>(paged)
        {
            Pagination = pagination
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var venta = await _ventaService.GetByIdAsync(id);
        if (venta is null)
            return NotFound(new ErrorResponse { Status = 404, Message = "Venta no encontrada." });

        return Ok(new ApiResponse<VentaDto>(venta));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VentaDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var created = await _ventaService.CreateAsync(dto);
        return Ok(new ApiResponse<VentaDto>(created));
    }

 
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] VentaDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var updated = await _ventaService.UpdateAsync(id, dto);
        return Ok(new ApiResponse<VentaDto>(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _ventaService.DeleteAsync(id);
        return NoContent();
    }
}