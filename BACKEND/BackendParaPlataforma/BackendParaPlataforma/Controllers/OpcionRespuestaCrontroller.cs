using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Infraestructure.Persistence;
using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpcionRespuestaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OpcionRespuestaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.OpcionesRespuesta.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var opcion = _context.OpcionesRespuesta.Find(id);
            if (opcion == null) return NotFound();
            return Ok(opcion);
        }

        [HttpPost]
        public IActionResult Create(OpcionRespuesta opcion)
        {
            _context.OpcionesRespuesta.Add(opcion);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = opcion.IdOpcion }, opcion);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OpcionRespuesta opcion)
        {
            var dbOpcion = _context.OpcionesRespuesta.Find(id);
            if (dbOpcion == null) return NotFound();

            dbOpcion.TextoOpcion = opcion.TextoOpcion;
            dbOpcion.EsCorrecta = opcion.EsCorrecta;
            dbOpcion.IdPregunta = opcion.IdPregunta;

            _context.SaveChanges();
            return Ok(dbOpcion);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var opcion = _context.OpcionesRespuesta.Find(id);
            if (opcion == null) return NotFound();

            _context.OpcionesRespuesta.Remove(opcion);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
