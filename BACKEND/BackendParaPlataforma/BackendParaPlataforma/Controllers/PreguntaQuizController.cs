using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Infraestructure.Persistence;
using BackendParaPlataforma.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntaQuizController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PreguntaQuizController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.PreguntasQuiz
                .Include(p => p.Opciones)
                .ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pregunta = _context.PreguntasQuiz
                .Include(p => p.Opciones)
                .FirstOrDefault(p => p.IdPregunta == id);

            if (pregunta == null) return NotFound();
            return Ok(pregunta);
        }

        [HttpPost]
        public IActionResult Create(PreguntaQuiz pregunta)
        {
            _context.PreguntasQuiz.Add(pregunta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = pregunta.IdPregunta }, pregunta);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PreguntaQuiz pregunta)
        {
            var dbPregunta = _context.PreguntasQuiz.Find(id);
            if (dbPregunta == null) return NotFound();

            dbPregunta.Pregunta = pregunta.Pregunta;
            dbPregunta.Orden = pregunta.Orden;
            dbPregunta.IdQuiz = pregunta.IdQuiz;

            _context.SaveChanges();
            return Ok(dbPregunta);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pregunta = _context.PreguntasQuiz.Find(id);
            if (pregunta == null) return NotFound();

            _context.PreguntasQuiz.Remove(pregunta);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
