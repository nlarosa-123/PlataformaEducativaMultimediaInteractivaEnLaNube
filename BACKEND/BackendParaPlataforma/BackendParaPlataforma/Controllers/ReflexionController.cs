using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReflexionMejoraController : ControllerBase
    {
        private readonly IReflexionMejoraRepository _repository;

        public ReflexionMejoraController(IReflexionMejoraRepository repository)
        {
            _repository = repository;
        }

        // ?? GET: api/ReflexionMejora
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReflexionMejora>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // ?? GET: api/ReflexionMejora/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReflexionMejora>> GetById(int id)
        {
            var reflexion = await _repository.GetByIdAsync(id);

            if (reflexion == null)
                return NotFound($"No se encontró la reflexión con ID {id}");

            return Ok(reflexion);
        }

        // ?? GET: api/ReflexionMejora/diario/10
        [HttpGet("diario/{diarioId}")]
        public async Task<ActionResult<ReflexionMejora>> GetByDiario(int diarioId)
        {
            var reflexion = await _repository.GetByDiarioIdAsync(diarioId);

            if (reflexion == null)
                return NotFound("No hay reflexión para este diario");

            return Ok(reflexion);
        }

        // ?? POST: api/ReflexionMejora
        [HttpPost]
        public async Task<ActionResult<ReflexionMejora>> Create([FromBody] ReflexionMejora reflexion)
        {
            if (reflexion == null)
                return BadRequest("Datos inválidos");

            var created = await _repository.CreateAsync(reflexion);

            return CreatedAtAction(nameof(GetById), new { id = created.Id_Reflexion }, created);
        }

        // ?? PUT: api/ReflexionMejora/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReflexionMejora reflexion)
        {
            if (id != reflexion.Id_Reflexion)
                return BadRequest("El ID no coincide");

            var updated = await _repository.UpdateAsync(reflexion);

            if (!updated)
                return NotFound($"No se encontró la reflexión con ID {id}");

            return NoContent();
        }

        // ?? DELETE: api/ReflexionMejora/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound($"No se encontró la reflexión con ID {id}");

            return NoContent();
        }
    }
}