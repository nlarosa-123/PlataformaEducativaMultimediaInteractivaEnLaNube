using BackendParaPlataforma.cmds;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgresoModuloUsuarioController : ControllerBase
    {
        private readonly IProgresoModuloUsuarioRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProgresoModuloUsuarioController(
    IProgresoModuloUsuarioRepository repository,
    IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/progresomodulousuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgresoModuloUsuarioDto>>> ObtenerTodos()
        {
            var lista = await _repository.ObtenerTodosAsync();

            var dtos = lista.Select(x => new ProgresoModuloUsuarioDto
            {
                Id = x.Id,
                IdUsuario = x.IdUsuario,
                IdModulo = x.IdModulo,
                Porcentaje = x.Porcentaje,
                Completado = x.Completado,
                UltimaLeccion = x.UltimaLeccion
            });

            return Ok(dtos);
        }

        // GET: api/progresomodulousuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgresoModuloUsuarioDto>> ObtenerPorId(int id)
        {
            var progreso = await _repository.ObtenerPorIdAsync(id);

            if (progreso == null)
                return NotFound();

            var dto = new ProgresoModuloUsuarioDto
            {
                Id = progreso.Id,
                IdUsuario = progreso.IdUsuario,
                IdModulo = progreso.IdModulo,
                Porcentaje = progreso.Porcentaje,
                Completado = progreso.Completado,
                UltimaLeccion = progreso.UltimaLeccion
            };

            return Ok(dto);
        }

        // POST: api/progresomodulousuario
        [HttpPost]
        public async Task<ActionResult<ProgresoModuloUsuarioDto>> Crear(
    [FromBody] CrearProgresoModuloUsuarioCommand cmd)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(cmd.IdUsuario);

            if (usuario == null)
                return BadRequest("El usuario no existe");

            var entidad = new ProgresoModuloUsuario
            {
                IdUsuario = cmd.IdUsuario,
                IdModulo = cmd.IdModulo,
                Porcentaje = cmd.Porcentaje,
                UltimaLeccion = cmd.UltimaLeccion,
                Completado = cmd.Porcentaje >= 100
            };

            await _repository.CrearAsync(entidad);
            await _repository.SaveChangesAsync();

            var dto = new ProgresoModuloUsuarioDto
            {
                Id = entidad.Id,
                IdUsuario = entidad.IdUsuario,
                IdModulo = entidad.IdModulo,
                Porcentaje = entidad.Porcentaje,
                Completado = entidad.Completado,
                UltimaLeccion = entidad.UltimaLeccion
            };

            return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, dto);
        }

        // PUT: api/progresomodulousuario/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProgresoModuloUsuarioDto>> Actualizar(
            int id,
            [FromBody] CrearProgresoModuloUsuarioCommand cmd)
        {
            var progreso = await _repository.ObtenerPorIdAsync(id);

            if (progreso == null)
                return NotFound();

            progreso.IdUsuario = cmd.IdUsuario;
            progreso.IdModulo = cmd.IdModulo;
            progreso.Porcentaje = cmd.Porcentaje;
            progreso.UltimaLeccion = cmd.UltimaLeccion;
            progreso.Completado = cmd.Porcentaje >= 100;

            _repository.Actualizar(progreso);
            await _repository.SaveChangesAsync();

            var dto = new ProgresoModuloUsuarioDto
            {
                Id = progreso.Id,
                IdUsuario = progreso.IdUsuario,
                IdModulo = progreso.IdModulo,
                Porcentaje = progreso.Porcentaje,
                Completado = progreso.Completado,
                UltimaLeccion = progreso.UltimaLeccion
            };

            return Ok(dto);
        }

        // DELETE: api/progresomodulousuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var progreso = await _repository.ObtenerPorIdAsync(id);

            if (progreso == null)
                return NotFound();

            _repository.Eliminar(progreso);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        #region usuario

        // progreso de todos los módulos de un usuario
        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<ProgresoModuloUsuarioDto>>> ObtenerPorUsuario(int idUsuario)
        {
            var lista = await _repository.ObtenerPorUsuarioAsync(idUsuario);

            var dtos = lista.Select(x => new ProgresoModuloUsuarioDto
            {
                Id = x.Id,
                IdUsuario = x.IdUsuario,
                IdModulo = x.IdModulo,
                Porcentaje = x.Porcentaje,
                Completado = x.Completado,
                UltimaLeccion = x.UltimaLeccion
            });

            return Ok(dtos);
        }

        // progreso de un usuario en un módulo específico
        [HttpGet("{idUsuario}/{idModulo}")]
        public async Task<ActionResult<ProgresoModuloUsuarioDto>> Obtener(int idUsuario, int idModulo)
        {
            var progreso = await _repository.ObtenerAsync(idUsuario, idModulo);

            if (progreso == null)
                return NotFound();

            var dto = new ProgresoModuloUsuarioDto
            {
                Id = progreso.Id,
                IdUsuario = progreso.IdUsuario,
                IdModulo = progreso.IdModulo,
                Porcentaje = progreso.Porcentaje,
                Completado = progreso.Completado,
                UltimaLeccion = progreso.UltimaLeccion
            };

            return Ok(dto);
        }

        #endregion
    }
}