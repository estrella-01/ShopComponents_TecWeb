using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Services.Interfaces;

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

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductoDto dto)
    {
        var producto = _mapper.Map<Producto>(dto);

        await _service.InsertAsync(producto);

        var result = _mapper.Map<ProductoDto>(producto);

        return Ok(result);
    }


}