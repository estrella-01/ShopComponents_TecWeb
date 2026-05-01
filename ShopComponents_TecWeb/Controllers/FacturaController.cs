using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.CustomEntities;
using ShopComponents.Core.DTOs;
using ShopComponents.Services.Interfaces;
using ShopComponents.Services.Validators;

namespace ShopComponents.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacturaController : ControllerBase
{
    private readonly IFacturaService _facturaService;
    private readonly FacturaDtoValidator _validator;

    public FacturaController(IFacturaService facturaService, FacturaDtoValidator validator)
    {
        _facturaService = facturaService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var facturas = await _facturaService.GetAllAsync();
        return Ok(new ApiResponse<IEnumerable<FacturaDto>>(facturas));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var factura = await _facturaService.GetByIdAsync(id);
        if (factura is null)
            return NotFound(new ErrorResponse { Status = 404, Message = "Factura no encontrada." });

        return Ok(new ApiResponse<FacturaDto>(factura));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FacturaDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var created = await _facturaService.CreateAsync(dto);
        return Ok(new ApiResponse<FacturaDto>(created));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _facturaService.DeleteAsync(id);
        return NoContent();
    }
}