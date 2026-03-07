using AutoMapper;
using BackendParaPlataforma.cmds;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using BackendParaPlataforma.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public AuthController(
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            JwtService jwtService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDto>> Register(CrearUsuarioCommand command)
        {
            var existingUser = await _usuarioRepository.GetByEmailAsync(command.Email);

            if (existingUser != null)
                return BadRequest("El email ya está registrado");

            var usuario = _mapper.Map<Usuario>(command);

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
            usuario.FechaRegistro = DateTime.UtcNow;
            usuario.Activo = true;

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(usuarioDto);
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);

            if (usuario == null)
                return Unauthorized("Usuario no encontrado");

            bool validPassword = BCrypt.Net.BCrypt.Verify(
                loginDto.Password,
                usuario.PasswordHash
            );

            if (!validPassword)
                return Unauthorized("Contraseña incorrecta");

            var token = _jwtService.GenerateToken(usuario);

            return Ok(new { token });
        }
    }
}