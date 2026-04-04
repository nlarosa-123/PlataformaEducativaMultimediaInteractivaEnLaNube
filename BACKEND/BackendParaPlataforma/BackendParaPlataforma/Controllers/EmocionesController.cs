using AutoMapper;
using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace BackendParaPlataforma.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmocionesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmocionesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/emociones
        [HttpGet]
        public ActionResult<List<EmocionesDto>> ObtenerEmociones()
        {
            var emociones = _context.Emociones.ToList();
            var emocionesDto = _mapper.Map<List<EmocionesDto>>(emociones);

            return Ok(emocionesDto);
        }
    }
}
