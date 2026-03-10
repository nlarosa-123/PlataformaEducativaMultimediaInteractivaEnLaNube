using Microsoft.AspNetCore.Mvc;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.cmds;
using BackendParaPlataforma.Infraestructure.Repositories;

namespace BackendParaPlataforma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmocionesController : ControllerBase
    {
        private readonly IEmocionesRepository _emocionesRepository;

        public EmocionesController(IEmocionesRepository emocionesRepository)
        {
            _emocionesRepository = emocionesRepository;
        }

        // GET: api/emociones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmocionesDto>>> Get()
        {
            var emociones = await _emocionesRepository.GetAllAsync();

            var emocionesDto = emociones.Select(e => new EmocionesDto
            {
                IdEmocion = e.IdEmocion,
                Nombre = e.Nombre,
                Emoji = e.Emoji,
                Valor = e.Valor
            });

            return Ok(emocionesDto);
        }

        // GET api/emociones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmocionesDto>> Get(int id)
        {
            var emocion = await _emocionesRepository.GetByIdAsync(id);

            if (emocion == null)
                return NotFound();

            var dto = new EmocionesDto
            {
                IdEmocion = emocion.IdEmocion,
                Nombre = emocion.Nombre,
                Emoji = emocion.Emoji,
                Valor = emocion.Valor
            };

            return Ok(dto);
        }

        // POST api/emociones
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CrearEmocionCommand command)
        {
            var emocion = new Emociones
            {
                Nombre = command.Nombre,
                Emoji = command.Emoji,
                Valor = command.Valor
            };

            await _emocionesRepository.AddAsync(emocion);
            await _emocionesRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = emocion.IdEmocion }, emocion);
        }

        // PUT api/emociones/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CrearEmocionCommand command)
        {
            var emocion = await _emocionesRepository.GetByIdAsync(id);

            if (emocion == null)
                return NotFound();

            emocion.Nombre = command.Nombre;
            emocion.Emoji = command.Emoji;
            emocion.Valor = command.Valor;

            await _emocionesRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/emociones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var emocion = await _emocionesRepository.GetByIdAsync(id);

            if (emocion == null)
                return NotFound();

            await _emocionesRepository.DeleteAsync(emocion);
            await _emocionesRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}