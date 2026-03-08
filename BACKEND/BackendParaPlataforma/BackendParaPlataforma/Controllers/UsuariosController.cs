using AutoMapper;
using BackendParaPlataforma.cmds;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        // GET api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                return NotFound();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(usuarioDto);
        }

        // POST api/usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> CrearUsuario(CrearUsuarioCommand command)
        {
            var usuario = _mapper.Map<Usuario>(command);

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
            usuario.FechaRegistro = DateTime.UtcNow;
            usuario.Activo = true;

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuarioDto);
        }
    }
}