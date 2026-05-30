using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopComponents.Core.Entities;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUsuarioService _usuarioService;

    public TokenController(IConfiguration configuration, IUsuarioService usuarioService)
    {
        _configuration = configuration;
        _usuarioService = usuarioService;
    }

    /// <summary>Obtiene un token JWT para autenticación</summary>
    [HttpPost]
    public async Task<IActionResult> Authentication([FromBody] UserLogin userLogin)
    {
        var user = await _usuarioService.GetByCredentialsAsync(userLogin);
        if (user is null) return Unauthorized(new { message = "Credenciales inválidas." });

        var token = GenerateToken(user);
        return Ok(new { token });
    }

    private string GenerateToken(Usuario usuario)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(credentials);

        var claims = new[]
        {
            new Claim("Id", usuario.Id.ToString()),
            new Claim("Nombre", usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Email ?? ""),
            new Claim(ClaimTypes.Role, usuario.Rol ?? "Consumer"),
        };

        var payload = new JwtPayload(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(8)
        );

        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(header, payload));
    }
}