using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestaUsuarioQuizController : ControllerBase
    {
        private readonly IRespuestaUsuarioQuizRepository _repository;

        public RespuestaUsuarioQuizController(IRespuestaUsuarioQuizRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET: api/respuestausuarioquiz
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var respuestas = await _repository.GetAllAsync();
            return Ok(respuestas);
        }

        // 🔹 GET: api/respuestausuarioquiz/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var respuesta = await _repository.GetByIdAsync(id);

            if (respuesta == null)
                return NotFound(new { message = "Respuesta no encontrada" });

            return Ok(respuesta);
        }

        // 🔹 GET: api/respuestausuarioquiz/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var respuestas = await _repository.GetByUsuarioAsync(usuarioId);
            return Ok(respuestas);
        }

        // 🔹 GET: api/respuestausuarioquiz/pregunta/{preguntaId}
        [HttpGet("pregunta/{preguntaId}")]
        public async Task<IActionResult> GetByPregunta(int preguntaId)
        {
            var respuestas = await _repository.GetByPreguntaAsync(preguntaId);
            return Ok(respuestas);
        }

        // 🔹 POST: api/respuestausuarioquiz
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RespuestaUsuarioQuiz respuesta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _repository.CreateAsync(respuesta);

                var dto = new RespuestaUsuarioQuizDto
                {
                    IdRespuesta = created.IdRespuesta,
                    IdUsuario = created.IdUsuario,
                    IdPregunta = created.IdPregunta,
                    IdOpcionElegida = created.IdOpcionElegida,
                    Correcta = created.Correcta,
                    FechaRespuesta = created.FechaRespuesta
                };

                return CreatedAtAction(nameof(GetById), new { id = dto.IdRespuesta }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 🔹 PUT: api/respuestausuarioquiz/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RespuestaUsuarioQuiz respuesta)
        {
            if (id != respuesta.IdRespuesta)
                return BadRequest(new { message = "El ID no coincide" });

            try
            {
                var updated = await _repository.UpdateAsync(respuesta);

                if (!updated)
                    return NotFound(new { message = "Respuesta no encontrada" });

                return Ok(new { message = "Respuesta actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 🔹 DELETE: api/respuestausuarioquiz/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Respuesta no encontrada" });

            return Ok(new { message = "Respuesta eliminada correctamente" });
        }

        // 🔥🔥 ENDPOINT PRO: Resolver quiz completo
        // POST: api/respuestausuarioquiz/resolver
        [HttpPost("resolver")]
        public async Task<IActionResult> ResolverQuiz(
            int usuarioId,
            int quizId,
            [FromBody] Dictionary<int, int> respuestas)
        {
            /*
             * respuestas:
             * clave = IdPregunta
             * valor = IdOpcionElegida
             */

            int total = respuestas.Count;
            int correctas = 0;

            foreach (var item in respuestas)
            {
                var respuesta = new RespuestaUsuarioQuiz
                {
                    IdUsuario = usuarioId,
                    IdPregunta = item.Key,
                    IdOpcionElegida = item.Value
                };

                var result = await _repository.CreateAsync(respuesta);

                if (result.Correcta)
                    correctas++;
            }

            var score = total == 0 ? 0 : (correctas * 100) / total;

            return Ok(new
            {
                totalPreguntas = total,
                correctas = correctas,
                incorrectas = total - correctas,
                puntuacion = score,
                aprobado = score >= 70
            });
        }
    }
}
