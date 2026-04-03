using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Infraestructure.Persistence;
using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestaUsuarioQuizController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RespuestaUsuarioQuizController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.RespuestasUsuarioQuiz.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var respuesta = _context.RespuestasUsuarioQuiz.Find(id);
            if (respuesta == null) return NotFound();
            return Ok(respuesta);
        }

        [HttpPost]
        public IActionResult Create(RespuestaUsuarioQuiz respuesta)
        {
            _context.RespuestasUsuarioQuiz.Add(respuesta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = respuesta.IdRespuesta }, respuesta);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, RespuestaUsuarioQuiz respuesta)
        {
            var dbRespuesta = _context.RespuestasUsuarioQuiz.Find(id);
            if (dbRespuesta == null) return NotFound();

            dbRespuesta.IdUsuario = respuesta.IdUsuario;
            dbRespuesta.IdPregunta = respuesta.IdPregunta;
            dbRespuesta.IdOpcionElegida = respuesta.IdOpcionElegida;
            dbRespuesta.Correcta = respuesta.Correcta;
            dbRespuesta.FechaRespuesta = respuesta.FechaRespuesta;

            _context.SaveChanges();
            return Ok(dbRespuesta);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var respuesta = _context.RespuestasUsuarioQuiz.Find(id);
            if (respuesta == null) return NotFound();

            _context.RespuestasUsuarioQuiz.Remove(respuesta);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
