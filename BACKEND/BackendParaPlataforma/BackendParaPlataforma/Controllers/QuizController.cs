using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net.Quic;

namespace BackendParaPlataforma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository _repository;

        public QuizController(IQuizRepository repository)
        {
            _repository = repository;
        }

        // ?? GET: api/quiz
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var quizzes = await _repository.GetAllAsync();
            return Ok(quizzes);
        }

        // ?? GET: api/quiz/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var quiz = await _repository.GetByIdAsync(id);

            if (quiz == null)
                return NotFound(new { message = "Quiz no encontrado" });

            return Ok(quiz);
        }

        // ?? GET: api/quiz/leccion/{leccionId}
        [HttpGet("leccion/{leccionId}")]
        public async Task<IActionResult> GetByLeccion(int leccionId)
        {
            var quiz = await _repository.GetByLeccionIdAsync(leccionId);

            if (quiz == null)
                return NotFound();

            var dto = new QuizDto
            {
                IdQuiz = quiz.IdQuiz,
                Titulo = quiz.Titulo,
                Descripcion = quiz.Descripcion,
                Preguntas = quiz.PreguntaQuizzes.Select(p => new PreguntaQuizDto
                {
                    IdPregunta = p.IdPregunta,
                    IdQuiz = p.IdQuiz
                }).ToList()
            };

            return Ok(dto);
        }

        // ?? POST: api/quiz
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _repository.CreateAsync(quiz);
                return CreatedAtAction(nameof(GetById), new { id = created.IdQuiz }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ?? PUT: api/quiz/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Quiz quiz)
        {
            if (id != quiz.IdQuiz)
                return BadRequest(new { message = "El ID no coincide" });

            var updated = await _repository.UpdateAsync(quiz);

            if (!updated)
                return NotFound(new { message = "Quiz no encontrado" });

            return Ok(new { message = "Quiz actualizado correctamente" });
        }

        // ?? DELETE: api/quiz/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Quiz no encontrado" });

            return Ok(new { message = "Quiz eliminado correctamente" });
        }

        [HttpPost("resolver")]
        public IActionResult ResolverQuiz(int quizId, Dictionary<int, int> respuestas)
        {
            // clave = preguntaId
            // valor = respuestaId seleccionada

            // ?? aquí validarías respuestas correctas
            // ?? calcular score

            return Ok(new { score = 80, aprobado = true });
        }
    }
}