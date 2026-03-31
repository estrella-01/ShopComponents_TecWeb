using Microsoft.AspNetCore.Mvc;
using ShopComponents.Services.Interfaces;

namespace ShopComponents_TecWeb.Api.Controllers;

[ApiController]
[Route("api/productos")]
public class ProductoController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductoController(IProductoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }
}