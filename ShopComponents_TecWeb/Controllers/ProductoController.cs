using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.CustomEntities;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Services.Interfaces;
using ShopComponents.Services.Validators;

namespace ShopComponents.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly IProductoService _service;
    private readonly IMapper _mapper;
    private readonly ProductoValidator _validator;

    public ProductoController(IProductoService service, IMapper mapper, ProductoValidator validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        var dto = _mapper.Map<IEnumerable<ProductoDto>>(data);
        return Ok(new ApiResponse<IEnumerable<ProductoDto>>(dto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);
        var dto = _mapper.Map<ProductoDto>(data);
        return Ok(new ApiResponse<ProductoDto>(dto));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductoDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var producto = _mapper.Map<Producto>(dto);
        await _service.InsertAsync(producto);

        var result = _mapper.Map<ProductoDto>(producto);
        return Ok(new ApiResponse<ProductoDto>(result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductoDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var producto = _mapper.Map<Producto>(dto);
        producto.Id = id;
        await _service.UpdateAsync(producto);

        return Ok(new ApiResponse<ProductoDto>(dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}