using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Services.Interfaces;
using ShopComponents_TecWeb.Api.Responses;

namespace ShopComponents_TecWeb.Api.Controllers;

[ApiController]
[Route("api/productos")]
public class ProductoController : ControllerBase
{
    private readonly IProductoService _service;
    private readonly IMapper _mapper;

    public ProductoController(IProductoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllAsync();
        var dto = _mapper.Map<IEnumerable<ProductoDto>>(data);

        return Ok(new ApiResponse<IEnumerable<ProductoDto>>
        {
            Success = true,
            Data = dto,
            Message = "Lista de productos"
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductoDto dto)
    {
        var producto = _mapper.Map<Producto>(dto);

        await _service.InsertAsync(producto);

        var result = _mapper.Map<ProductoDto>(producto);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound(new ApiResponse<string>
            {
                Success = false,
                Data = null,
                Message = "Producto no encontrado"
            });

        var dto = _mapper.Map<ProductoDto>(data);

        return Ok(new ApiResponse<ProductoDto>
        {
            Success = true,
            Data = dto,
            Message = "Producto encontrado"
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductoDto dto)
    {
        var producto = _mapper.Map<Producto>(dto);

        producto.Id = id; // 🔥 MUY IMPORTANTE

        await _service.UpdateAsync(producto);

        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok("Eliminado");
    }

    [HttpPost("proforma")]
    public async Task<IActionResult> CrearProforma(CrearProformaDto dto)
    {
        try
        {
            var result = await _service.CrearProforma(dto.ProductoId, dto.Cantidad);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = result,
                Message = "Proforma generada"
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