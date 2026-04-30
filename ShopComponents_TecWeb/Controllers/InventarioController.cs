using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.DTOs;
using ShopComponents.Services.Interfaces;
using ShopComponents_TecWeb.Api.Responses;

namespace ShopComponents_TecWeb.Api.Controllers;

[ApiController]
[Route("api/inventario")]
public class InventarioController : ControllerBase
{
    private readonly IInventarioService _service;
    private readonly IValidator<InventarioDto> _validator;

    public InventarioController(IInventarioService service, IValidator<InventarioDto> validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var data = await _service.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<InventarioDto>>
            {
                Success = true,
                Data = data,
                Message = "Lista de movimientos de inventario"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Data = null,
                Message = ex.Message
            });
        }
    }

    [HttpGet("producto/{productoId:int}")]
    public async Task<IActionResult> GetByProducto(int productoId)
    {
        try
        {
            var data = await _service.GetByProductoIdAsync(productoId);

            return Ok(new ApiResponse<IEnumerable<InventarioDto>>
            {
                Success = true,
                Data = data,
                Message = $"Movimientos del producto {productoId}"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Data = null,
                Message = ex.Message
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarMovimiento([FromBody] InventarioDto dto)
    {
        try
        {
            var validacion = await _validator.ValidateAsync(dto);

            if (!validacion.IsValid)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(" | ", validacion.Errors.Select(e => e.ErrorMessage))
                });
            }

            await _service.RegistrarMovimientoAsync(dto);

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Movimiento registrado y stock actualizado"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Data = null,
                Message = ex.Message
            });
        }
    }
}