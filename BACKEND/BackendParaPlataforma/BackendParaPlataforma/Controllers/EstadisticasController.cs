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
    public class EstadisticasController : ControllerBase
    {
        private readonly IEstadisticasRepository _statsRepository;

        public EstadisticasController(IEstadisticasRepository statsRepo)
        {
            _statsRepository = statsRepo;
        }


        // POST api/estadisticas (Create) 
        [HttpPost]
        public async Task<ActionResult<EstadisticaUsuarioDto>> Post([FromBody] CrearEstadisticaUsuarioCommand command)
        {

            //Esto habra que actualizarlo supongo
            var stats = new EstadisticaUsuario(command.IdUsuario);


            await _statsRepository.AddAsync(stats);
            await _statsRepository.SaveChangesAsync();


            return CreatedAtAction(nameof(Get), new { IdEstadistica = stats.IdEstadistica }, stats);
        }

        // Get api/estadisticas/userId (Read) 

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadisticaUsuarioDto>> Get(int userId)
        {
            var estadisticas = await _statsRepository.GetByIdAsync(userId); 

            if (estadisticas == null) {
                return NotFound(); 
            }

            var estadisticasDto =  new EstadisticaUsuarioDto
            {
                IdEstadistica = estadisticas.IdEstadistica,
                IdUsuario = estadisticas.IdUsuario,
                PorcentajeCoincidenciaIA = estadisticas.PorcentajeCoincidenciaIA,
                EmocionMasFrecuente = estadisticas.EmocionMasFrecuente,
                RachaDiasRegistrados = estadisticas.RachaDiasRegistrados,
                UltimaActualizacion = estadisticas.UltimaActualizacion
            };

            return Ok(estadisticasDto);
        }

        //PUT api/estadisticas/id (Update) 
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(int id, [FromBody] CrearEstadisticaUsuarioCommand command)
        {
            var stats = await _statsRepository.GetByIdAsync(id);
            if (stats == null)
                return NotFound();

            stats.ActualizarEstadisticas(stats.PorcentajeCoincidenciaIA, stats.EmocionMasFrecuente,stats.RachaDiasRegistrados);
            await _statsRepository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/estadisticas/id (Delete) 
        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id)
        {

            var stats = await _statsRepository.GetByIdAsync(id);

            if (stats == null)
                return NotFound();

            await _statsRepository.DeleteAsync(stats);
            await _statsRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
