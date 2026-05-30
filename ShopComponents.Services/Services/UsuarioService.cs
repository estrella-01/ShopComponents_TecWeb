using AutoMapper;
using ShopComponents.Core.DTOs;
using ShopComponents.Core.Entities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Services.Interfaces;

namespace ShopComponents.Services.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordService _passwordService;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository,
        IPasswordService passwordService, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _passwordService = passwordService;
        _mapper = mapper;
    }

    public async Task<Usuario?> GetByCredentialsAsync(UserLogin login)
    {
        var user = await _usuarioRepository.GetByEmailAsync(login.User);
        if (user is null) return null;
        return _passwordService.Check(user.Password!, login.Password) ? user : null;
    }

    public async Task RegisterAsync(UsuarioDto dto)
    {
        var usuario = _mapper.Map<Usuario>(dto);
        usuario.Password = _passwordService.Hash(dto.Password);
        usuario.Activo = true;
        await _usuarioRepository.AddAsync(usuario);
    }
}