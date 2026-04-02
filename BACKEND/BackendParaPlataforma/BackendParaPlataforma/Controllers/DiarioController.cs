using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Infraestructure.Repositories;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Dtos;

namespace BackendParaPlataforma.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class DiarioController : ControllerBase {
        private readonly IDiarioRepository _diarioRepository;

        public DiarioController(IDiarioRepository diarioRepository) {
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

        // Actualizar Diario
        [HttpPut("{idDiario}")]
        public async Task<IActionResult> ActualizarDiario(int idDiario, [FromBody] CreateDiarioDto dto) {

            var diario = new DiarioEmocional
            {
                Id_Diario = idDiario,
                Id_Usuario = dto.IdUsuario,
                Fecha = dto.Fecha,
                Id_Emocion_Usuario = dto.IdEmocionUsuario,
                Texto_Usuario = dto.TextoUsuario,
                Audio_Url = dto.AudioUrl
            };

            var actualizado = await _diarioRepository.ActualizarDiario(diario);

            if (!actualizado)
                return NotFound("No se pudo actualizar");

            return Ok("Diario actualizado");
        }

        // Eliminar Diario
        [HttpDelete("{idDiario}")]
        public async Task<IActionResult> EliminarDiario(int idDiario) {

            var eliminado = await _diarioRepository.EliminarDiario(idDiario);

            if (!eliminado)
                return NotFound("No se pudo eliminar");

            return Ok("Diario eliminado");
        }

    }
}
