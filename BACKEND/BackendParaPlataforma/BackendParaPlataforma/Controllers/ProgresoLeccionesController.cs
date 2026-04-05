using AutoMapper;
using BackendParaPlataforma.cmds;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;


namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgresoLeccionesController : ControllerBase
    {
        private readonly IProgresoLeccionesRepository _progresoRepository;

        public ProgresoLeccionesController(IProgresoLeccionesRepository progresoRepo)
        {
            _progresoRepository = progresoRepo;
        }

        // POST api/progreso (Create) 
        [HttpPost]
        public async Task<ActionResult<ProgresoLeccionesUsuarioDto>> Post([FromBody] CrearProgresoLeccionesCommand command)
        {
            var progreso = new ProgresoLeccionesUsuario(); 


            await _progresoRepository.AddAsync(progreso);
            await _progresoRepository.SaveChangesAsync();


            return CreatedAtAction(nameof(Get), new { id = progreso.Id }, progreso);
        }

        // Get api/modulos/Id (Read) 

        [HttpGet("{id}")]
        public async Task<ActionResult<ProgresoLeccionesUsuarioDto>> Get(int id)
        {
            var progreso = await _progresoRepository.GetByIdAsync(id);

            if (progreso == null)
            {
                return NotFound();
            }

            var progresoDto = new ProgresoLeccionesUsuarioDto
            {
                Id = progreso.Id,
                IdUsuario = progreso.IdUsuario,
                IdLeccion = progreso.IdLeccion, 
                Completada  = progreso.Completada, 
                FechaCompletada = progreso.FechaCompletada, 
                TiempoVisualizado = progreso.TiempoVisualizado
            };

            return Ok(progresoDto);
        }

        //PUT api/modulos/id (Update) 
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(int id, [FromBody] CrearProgresoLeccionesCommand command)
        {
            var progreso = await _progresoRepository.GetByIdAsync(id);
            if (progreso == null)
                return NotFound();

            progreso.LeccionCompletada();
            progreso.SetFecha(); 

            await _progresoRepository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/modulos/id (Delete) 
        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id)
        {

            var progreso = await _progresoRepository.GetByIdAsync(id);

            if (progreso == null)
                return NotFound();

            await _progresoRepository.DeleteAsync(progreso);
            await _progresoRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}