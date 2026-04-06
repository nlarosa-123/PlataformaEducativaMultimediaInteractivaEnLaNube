using BackendParaPlataforma.Entities;
using BackendParaPlataforma.FuncionesAux;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadisticaUsuarioController : ControllerBase
    {
        private readonly IEstadisticaUsuarioRepository _repository;
        private readonly MetodosAux _metodosAux;

        public EstadisticaUsuarioController(IEstadisticaUsuarioRepository repository, MetodosAux metodosAux)
        {
            _repository = repository;
            _metodosAux = metodosAux;
        }

        [HttpPost("actualizar/{idUsuario}")]
        public async Task<IActionResult> ActualizarEstadisticas(int idUsuario)
        {
            await _metodosAux.CrearActualizarEstUsuario(idUsuario);
            return Ok("Estadísticas recalculadas correctamente");
        }

        // ?? GET: api/EstadisticaUsuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadisticaUsuario>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // ?? GET: api/EstadisticaUsuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadisticaUsuario>> GetById(int id)
        {
            var estadistica = await _repository.GetByIdAsync(id);

            if (estadistica == null)
                return NotFound($"No se encontró la estadística con ID {id}");

            return Ok(estadistica);
        }

        // ?? GET: api/EstadisticaUsuario/usuario/1
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<EstadisticaUsuario>> GetByUsuario(int usuarioId)
        {
            await _metodosAux.CrearActualizarEstUsuario(usuarioId);

            var estadistica = await _repository.GetByUsuarioIdAsync(usuarioId);

            if (estadistica == null)
                return NotFound("El usuario aún no tiene estadísticas");

            return Ok(estadistica);
        }

        // ?? POST: api/EstadisticaUsuario
        [HttpPost]
        public async Task<ActionResult<EstadisticaUsuario>> Create([FromBody] EstadisticaUsuario estadistica)
        {
            if (estadistica == null)
                return BadRequest("Datos inválidos");

            var created = await _repository.CreateAsync(estadistica);

            return CreatedAtAction(nameof(GetById), new { id = created.IdEstadistica }, created);
        }

        // ?? PUT: api/EstadisticaUsuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EstadisticaUsuario estadistica)
        {
            if (id != estadistica.IdEstadistica)
                return BadRequest("El ID no coincide");

            var updated = await _repository.UpdateAsync(estadistica);

            if (!updated)
                return NotFound($"No se encontró la estadística con ID {id}");

            return NoContent();
        }

        // ?? POST: api/EstadisticaUsuario/upsert
        [HttpPost("upsert")]
        public async Task<IActionResult> Upsert([FromBody] EstadisticaUsuario estadistica)
        {
            if (estadistica == null)
                return BadRequest("Datos inválidos");

            await _repository.UpsertAsync(estadistica);

            return Ok("Estadísticas actualizadas correctamente");
        }

        // ?? DELETE: api/EstadisticaUsuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound($"No se encontró la estadística con ID {id}");

            return NoContent();
        }
    }
}