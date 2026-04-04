using AutoMapper;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiarioController : ControllerBase
    {
        private readonly IDiarioRepository _diarioRepository;
        private readonly IMapper _mapper;

        public DiarioController(IDiarioRepository diarioRepository, IMapper mapper)
        {
            _diarioRepository = diarioRepository;
            _mapper = mapper;
        }

        // POST api/diario
        [HttpPost]
        public async Task<ActionResult<DiarioEmocionalDto>> CrearEntrada(CrearDiarioDto command)
        {
            var diarioEmocional = _mapper.Map<DiarioEmocional>(command);
            diarioEmocional.Fecha = DateTime.UtcNow;

            await _diarioRepository.AddAsync(diarioEmocional);
            await _diarioRepository.SaveChangesAsync();

            var diarioDto = _mapper.Map<DiarioEmocionalDto>(diarioEmocional);

            return CreatedAtAction(nameof(ObtenerEntradas), new { usuarioId = diarioEmocional.UsuarioId }, diarioDto);
        }

        // GET api/diario/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<DiarioEmocionalDto>>> ObtenerEntradas(int usuarioId)
        {
            var entradas = await _diarioRepository.GetByUsuarioIdAsync(usuarioId);
            var entradasDto = _mapper.Map<List<DiarioEmocionalDto>>(entradas);

            return Ok(entradasDto);
        }
    }
}
