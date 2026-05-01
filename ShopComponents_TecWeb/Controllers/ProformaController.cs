using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.CustomEntities;
using ShopComponents.Core.DTOs;
using ShopComponents.Services.Interfaces;
using ShopComponents.Services.Validators;

namespace ShopComponents.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProformaController : ControllerBase
{
    private readonly IProformaService _proformaService;
    private readonly ProformaDtoValidator _validator;

    public ProformaController(IProformaService proformaService, ProformaDtoValidator validator)
    {
        _proformaService = proformaService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _proformaService.GetAllAsync();
        return Ok(new ApiResponse<IEnumerable<ProformaDto>>(data));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var proforma = await _proformaService.GetByIdAsync(id);
        if (proforma is null)
            return NotFound(new ErrorResponse { Status = 404, Message = "Proforma no encontrada." });

        return Ok(new ApiResponse<ProformaDto>(proforma));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProformaDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var created = await _proformaService.CreateAsync(dto);
        return Ok(new ApiResponse<ProformaDto>(created));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _proformaService.DeleteAsync(id);
        return NoContent();
    }
}