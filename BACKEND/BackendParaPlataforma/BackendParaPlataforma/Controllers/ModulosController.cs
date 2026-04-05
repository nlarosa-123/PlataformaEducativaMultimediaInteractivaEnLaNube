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
    public class ModulosController : ControllerBase
    {
        private readonly IModulosRepository _modsRepository;

        public ModulosController(IModulosRepository modsRepo)
        {
            _modsRepository = modsRepo;
        }


        // POST api/modulos (Create) 
        [HttpPost]
        public async Task<ActionResult<ModulosDto>> Post([FromBody] CrearModuloCommand command)
        {

            //Esto habra que actualizarlo supongo
            var mods = new Modulos(command.Titulo, command.Descripcion);


            await _modsRepository.AddAsync(mods);
            await _modsRepository.SaveChangesAsync();


            return CreatedAtAction(nameof(Get), new { id = mods.IdModulo }, mods);
        }

        // Get api/modulos/Id (Read) 

        [HttpGet("{id}")]
        public async Task<ActionResult<ModulosDto>> Get(int id)
        {
            var mods = await _modsRepository.GetByIdAsync(id);

            if (mods == null)
            {
                return NotFound(); 
            }

            var modsDto =  new ModulosDto
            {
                IdModulo = mods.IdModulo,
                Titulo = mods.Titulo, 
                Descripcion = mods.Descripcion
            };

            return Ok(modsDto);
        }

        //PUT api/modulos/id (Update) 
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(int id, [FromBody] CrearModuloCommand command)
        {
            var mods = await _modsRepository.GetByIdAsync(id);
            if (mods == null)
                return NotFound();

            await _modsRepository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/modulos/id (Delete) 
        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id)
        {

            var mods = await _modsRepository.GetByIdAsync(id);

            if (mods == null)
                return NotFound();

            await _modsRepository.DeleteAsync(mods);
            await _modsRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}