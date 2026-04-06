using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpcionRespuestaController : ControllerBase
    {
        private readonly IOpcionRespuestaRepository _repository;

        public OpcionRespuestaController(IOpcionRespuestaRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET: api/opcionrespuesta
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var opciones = await _repository.GetAllAsync();
            return Ok(opciones);
        }

        // 🔹 GET: api/opcionrespuesta/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var opcion = await _repository.GetByIdAsync(id);

            if (opcion == null)
                return NotFound(new { message = "Opción no encontrada" });

            return Ok(opcion);
        }

        // 🔹 GET: api/opcionrespuesta/pregunta/{preguntaId}
        [HttpGet("pregunta/{preguntaId}")]
        public async Task<IActionResult> GetByPregunta(int preguntaId)
        {
            var opciones = await _repository.GetByPreguntaIdAsync(preguntaId);
            return Ok(opciones);
        }

        // 🔹 GET: api/opcionrespuesta/pregunta/{preguntaId}/correcta
        [HttpGet("pregunta/{preguntaId}/correcta")]
        public async Task<IActionResult> GetCorrecta(int preguntaId)
        {
            var opcion = await _repository.GetCorrectaByPreguntaIdAsync(preguntaId);

            if (opcion == null)
                return NotFound(new { message = "No hay respuesta correcta definida" });

            return Ok(opcion);
        }

        // 🔹 POST: api/opcionrespuesta
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OpcionRespuesta opcion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(opcion.TextoOpcion))
                return BadRequest("El texto de la opción es obligatorio");

            try
            {
                var created = await _repository.CreateAsync(opcion);
                return CreatedAtAction(nameof(GetById), new { id = created.IdOpcion }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 🔹 PUT: api/opcionrespuesta/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OpcionRespuesta opcion)
        {
            if (id != opcion.IdOpcion)
                return BadRequest(new { message = "El ID no coincide" });

            try
            {
                var updated = await _repository.UpdateAsync(opcion);

                if (!updated)
                    return NotFound(new { message = "Opción no encontrada" });

                return Ok(new { message = "Opción actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 🔹 DELETE: api/opcionrespuesta/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Opción no encontrada" });

            return Ok(new { message = "Opción eliminada correctamente" });
        }
    }
}
