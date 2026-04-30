using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.DTOs;
using ShopComponents.Services.Interfaces;
using ShopComponents_TecWeb.Api.Responses;

namespace ShopComponents_TecWeb.Api.Controllers;

[ApiController]
[Route("api/facturas")]
public class FacturasController : ControllerBase
{
    private readonly IFacturaService _service;
    private readonly IValidator<FacturaDto> _validator;

    public FacturasController(IFacturaService service, IValidator<FacturaDto> validator)
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

            return Ok(new ApiResponse<IEnumerable<FacturaDto>>
            {
                Success = true,
                Data = data,
                Message = "Lista de facturas"
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Data = null,
                    Message = "Factura no encontrada"
                });

            return Ok(new ApiResponse<FacturaDto>
            {
                Success = true,
                Data = data,
                Message = "Factura encontrada"
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
    public async Task<IActionResult> Create([FromBody] FacturaDto dto)
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

            var result = await _service.CreateAsync(dto);

            return Ok(new ApiResponse<FacturaDto>
            {
                Success = true,
                Data = result,
                Message = "Factura creada correctamente"
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] FacturaDto dto)
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

            var result = await _service.UpdateAsync(id, dto);

            return Ok(new ApiResponse<FacturaDto>
            {
                Success = true,
                Data = result,
                Message = "Factura actualizada correctamente"
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Factura eliminada correctamente"
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
}