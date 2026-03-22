using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Repositories;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Dtos;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiarioController : ControllerBase {
        private readonly IDiarioRepository _diarioRepository;

        public DiarioController(IDiarioRepository diarioRepository)
        {
            _diarioRepository = diarioRepository;
        }

        // Crear un nuevo diario emocional
        [HttpPost]
        public async Task<IActionResult> CrearDiario([FromBody] CreateDiarioDto dto) {
            var diario = new DiarioEmocional {

                Id_Usuario = dto.IdUsuario,
                Fecha = dto.Fecha,
                Id_Emocion_Usuario = dto.IdEmocionUsuario,
                Texto_Usuario = dto.TextoUsuario,
                Audio_Url = dto.AudioUrl,
                Fecha_Creacion = DateTime.Now
            };

            var resultado = await _diarioRepository.CrearDiario(diario);

            return Ok(resultado);
        }

        /*var userId = int.Parse(User.FindFirst("id")!.Value);

        _context.Auditorias.Add(new Auditoria {
            IdUsuario = userId,
            Accion = "CREAR_DIARIO",
            Entidad = "DiarioEmocional",
            Detalle = "Usuario creó un diario",
            Fecha = DateTime.UtcNow
        });*/

        // Obtener todos los diarios de un usuario
        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerDiariosUsuario(int idUsuario) {
            var diarios = await _diarioRepository.ObtenerDiariosUsuario(idUsuario);

            return Ok(diarios);
        }

        // Obtener un diario específico
        [HttpGet("{idDiario}")]
        public async Task<IActionResult> ObtenerDiario(int idDiario) {
            var diario = await _diarioRepository.ObtenerDiarioPorId(idDiario);

            if (diario == null)
                return NotFound("Diario no encontrado");

            return Ok(diario);
        }

    }
}

/**
using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Repositories;

[ApiController]
[Route("api/diario")]
public class DiarioController : ControllerBase {

    private readonly IDiarioRepository _diarioRepository;

    public DiarioController(IDiarioRepository diarioRepository) {

        _diarioRepository = diarioRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CrearDiario([FromBody] DiarioEmocional diario) {

        var resultado = await _diarioRepository.CrearDiario(diario);
        return Ok(resultado);
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<IActionResult> ObtenerDiariosUsuario(int idUsuario) {

        var diarios = await _diarioRepository.ObtenerDiariosUsuario(idUsuario);
        return Ok(diarios);
    }

    [HttpGet("{idDiario}")]
    public async Task<IActionResult> ObtenerDiario(int idDiario) {

        var diario = await _diarioRepository.ObtenerDiarioPorId(idDiario);

        if (diario == null)
            return NotFound();

        return Ok(diario);
    }

}   */
