using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntaQuizController : ControllerBase
    {
        private readonly IPreguntaQuizRepository _repository;

        public PreguntaQuizController(IPreguntaQuizRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET: api/preguntaquiz
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var preguntas = await _repository.GetAllAsync();
            return Ok(preguntas);
        }

        // 🔹 GET: api/preguntaquiz/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pregunta = await _repository.GetByIdAsync(id);

            if (pregunta == null)
                return NotFound(new { message = "Pregunta no encontrada" });

            return Ok(pregunta);
        }

        // 🔹 GET: api/preguntaquiz/quiz/{quizId}
        [HttpGet("quiz/{quizId}")]
        public async Task<IActionResult> GetByQuiz(int quizId)
        {
            var preguntas = await _repository.GetByQuizIdAsync(quizId);

            var dto = preguntas.Select(p => new PreguntaQuizDto
            {
                IdPregunta = p.IdPregunta,
                Pregunta = p.Pregunta
            });

            return Ok(dto);
        }

        // 🔹 POST: api/preguntaquiz
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PreguntaQuiz pregunta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(pregunta.Pregunta))
                return BadRequest("El texto de la pregunta es obligatorio");

            var created = await _repository.CreateAsync(pregunta);

            return CreatedAtAction(nameof(GetById), new { id = created.IdPregunta }, created);
        }

        // 🔹 PUT: api/preguntaquiz/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PreguntaQuiz pregunta)
        {
            if (id != pregunta.IdPregunta)
                return BadRequest(new { message = "El ID no coincide" });

            var updated = await _repository.UpdateAsync(pregunta);

            if (!updated)
                return NotFound(new { message = "Pregunta no encontrada" });

            return Ok(new { message = "Pregunta actualizada correctamente" });
        }

        // 🔹 DELETE: api/preguntaquiz/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Pregunta no encontrada" });

            return Ok(new { message = "Pregunta eliminada correctamente" });
        }

        // 🔥 Endpoint útil: reordenar preguntas
        // PUT: api/preguntaquiz/reordenar
        [HttpPut("reordenar")]
        public async Task<IActionResult> Reordenar([FromBody] List<PreguntaQuiz> preguntas)
        {
            foreach (var pregunta in preguntas)
            {
                await _repository.UpdateAsync(pregunta);
            }

            return Ok(new { message = "Orden actualizado correctamente" });
        }
    }
}
