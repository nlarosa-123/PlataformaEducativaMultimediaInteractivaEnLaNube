using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.cmds;
using BackendParaPlataforma.Infraestructure.Repositories;

namespace BackendParaPlataforma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeccionesController : ControllerBase
    {
        private readonly ILeccionesRepository _leccionesRepository;

        public LeccionesController(ILeccionesRepository leccionesRepository)
        {
            _leccionesRepository = leccionesRepository;
        }

        // GET api/lecciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeccionesDto>> Get(int id)
        {
            var leccion = await _leccionesRepository.GetByIdAsync(id);

            if (leccion == null)
                return NotFound();

            var dto = new LeccionesDto
            {
                   Id = leccion.Id, 
                   IdModulo = leccion.IdModulo, 
                   Titulo = leccion.Titulo, 
                   TipoContenido = leccion.TipoContenido, 
                   ContenidoTxt = leccion.ContenidoTxt, 
                   UrlVideo = leccion.UrlVideo, 
                   UrlAudio = leccion.UrlAudio, 
                   Orden = leccion.Orden, 
                   Duracion = leccion.Duracion
            };

            return Ok(dto);
        }

        // POST api/emociones
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CrearLeccionCommand command)
        {
            var leccion = new Lecciones(command.Titulo, command.ContenidoTxt, command.UrlVideo, command.UrlAudio, command.Duracion); 

            await _leccionesRepository.AddAsync(leccion);
            await _leccionesRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = leccion.Id }, leccion);
        }

        // PUT api/emociones/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CrearLeccionCommand command)
        {
            var leccion = await _leccionesRepository.GetByIdAsync(id);

            if (leccion == null)
                return NotFound();

            await _leccionesRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/lecciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var leccion = await _leccionesRepository.GetByIdAsync(id);

            if (leccion == null)
                return NotFound();

            await _leccionesRepository.DeleteAsync(leccion);
            await _leccionesRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}