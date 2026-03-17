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
        private readonly IStatsRepository _statsRepository;

        public EstadisticasController (IStatsRepository statsRepo)
        {
            _statsRepository = statsRepo; 
        }


        // POST api/estadisticas (Create) 
        [HttpPost]
        public async Task<ActionResult<EstadisticasDto>>Post([FromBody] CrearEstadisticasCommand command)
        {

            //Esto habra que actualizarlo supongo
            var stats = new Estadisticas(command.IdUsuario); 


            await _statsRepository.AddAsync(stats);
            await _statsRepository.SaveChangesAsync();


            return CreatedAtAction(nameof(Get), new { id = stats.Id }, stats);
        }
    


        // Get api/estadisticas/userId (Read) 

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadisticasDto>>Get(int userId)
        {
            var estadisticas = await _statsRepository.GetAllSync();

            var estadisticasDto = estadisticas.Select(e => new EstadisticasDto
            {
                Id= e.Id, 
                IdUsuario = e.IdUsuario, 
                CoincidenciaIa = e.CoincidenciaIa, 
                EmocionFrecuente = e.EmocionFrecuente, 
                RachaDias = e.RachaDias, 
                UltimaAct = e.UltimaAct
            });

            return Ok(estadisticasDto); 
        }

        //PUT api/estadisticas/id (Update) 
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(int id, [FromBody] CrearEstadisticasCommand command)
        {
            var stats = await _statsRepository.GetByIdAsync(id);
            if (stats == null)
                return NotFound();

            stats.ActualizarRacha(command.RachaDias);
            stats.ActualizarFecha(command.UltimaAct);


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
