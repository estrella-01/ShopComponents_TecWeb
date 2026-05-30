using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopComponents.Core.CustomEntities;
using ShopComponents.Core.DTOs;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Api.Controllers;

[Authorize(Roles = "Administrator")]
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>Registra un nuevo usuario (solo Administrador)</summary>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UsuarioDto dto)
    {
        await _usuarioService.RegisterAsync(dto);
        return Ok(new ApiResponse<string>("Usuario registrado correctamente."));
    }
}