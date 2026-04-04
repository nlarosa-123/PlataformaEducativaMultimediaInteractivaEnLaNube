using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiarioEmocionalController : ControllerBase
    {
        private readonly IDiarioEmocionalRepository _repository;

        public DiarioEmocionalController(IDiarioEmocionalRepository repository)
        {
            _repository = repository;
        }

        // 📌 GET: api/DiarioEmocional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiarioEmocional>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // 📌 GET: api/DiarioEmocional/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiarioEmocional>> GetById(int id)
        {
            var diario = await _repository.GetByIdAsync(id);

            if (diario == null)
                return NotFound($"No se encontró el diario con ID {id}");

            return Ok(diario);
        }

        // 📊 GET: api/DiarioEmocional/usuario/1
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<DiarioListaDto>>> GetByUsuario(int usuarioId)
        {
            var diarios = await _repository.GetByUsuarioAsync(usuarioId);
            return Ok(diarios);
        }

        // 📅 GET: api/DiarioEmocional/usuario/1/fecha/2026-04-03
        [HttpGet("usuario/{usuarioId}/fecha")]
        public async Task<ActionResult<List<DiarioListaDto>>> GetByFecha(
    int usuarioId,
    [FromQuery] DateTime fecha)
        {
            var result = await _repository.GetByUsuarioYFechaAsync(usuarioId, fecha);
            return Ok(result);
        }

        // 🚀 GET: api/DiarioEmocional/usuario/1/latest
        [HttpGet("usuario/{usuarioId}/latest")]
        public async Task<ActionResult<DiarioEmocionalDto>> GetLatest(int usuarioId)
        {
            var diario = await _repository.GetLatestByUsuarioAsync(usuarioId);

            if (diario == null)
                return NotFound();

            return Ok(diario);
        }

        // 📌 POST: api/DiarioEmocional
        [HttpPost]
        public async Task<ActionResult<DiarioEmocional>> Create([FromBody] DiarioEmocional diario)
        {
            if (diario == null)
                return BadRequest("Datos inválidos");

            var created = await _repository.CreateAsync(diario);

            return CreatedAtAction(nameof(GetById), new { id = created.Id_Diario }, created);
        }

        // 📌 PUT: api/DiarioEmocional/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DiarioEmocional diario)
        {
            if (id != diario.Id_Diario)
                return BadRequest("El ID no coincide");

            var updated = await _repository.UpdateAsync(diario);

            if (!updated)
                return NotFound($"No se encontró el diario con ID {id}");

            return NoContent();
        }

        // 📌 DELETE: api/DiarioEmocional/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound($"No se encontró el diario con ID {id}");

            return NoContent();
        }
        [HttpGet("usuario/{usuarioId}/hoy")]
        public async Task<ActionResult<DiarioEmocionalDto>> GetHoy(int usuarioId)
        {
            var diario = await _repository.GetHoyByUsuarioAsync(usuarioId);

            if (diario == null)
                return NotFound("No hay registro hoy");

            return Ok(diario);
        }
        
    }
}