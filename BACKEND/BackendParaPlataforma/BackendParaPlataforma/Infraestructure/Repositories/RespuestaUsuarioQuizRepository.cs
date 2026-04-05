using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class RespuestaUsuarioQuizRepository : IRespuestaUsuarioQuizRepository
    {
        private readonly AppDbContext _context;

        public RespuestaUsuarioQuizRepository(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todas
        public async Task<IEnumerable<RespuestaUsuarioQuiz>> GetAllAsync()
        {
            return await _context.RespuestaUsuarioQuizzes
                .Include(r => r.Usuario)
                .Include(r => r.Pregunta)
                .Include(r => r.OpcionRespuesta)
                .ToListAsync();
        }

        // 🔹 Obtener por ID
        public async Task<RespuestaUsuarioQuiz?> GetByIdAsync(int id)
        {
            return await _context.RespuestaUsuarioQuizzes
                .Include(r => r.Usuario)
                .Include(r => r.Pregunta)
                .Include(r => r.OpcionRespuesta)
                .FirstOrDefaultAsync(r => r.IdRespuesta == id);
        }

        // 🔹 Obtener respuestas por usuario
        public async Task<IEnumerable<RespuestaUsuarioQuiz>> GetByUsuarioAsync(int usuarioId)
        {
            return await _context.RespuestaUsuarioQuizzes
                .Include(r => r.Pregunta)
                .Include(r => r.OpcionRespuesta)
                .Where(r => r.IdUsuario == usuarioId)
                .ToListAsync();
        }

        // 🔹 Obtener respuestas por pregunta
        public async Task<IEnumerable<RespuestaUsuarioQuiz>> GetByPreguntaAsync(int preguntaId)
        {
            return await _context.RespuestaUsuarioQuizzes
                .Include(r => r.Usuario)
                .Where(r => r.IdPregunta == preguntaId)
                .ToListAsync();
        }

        // 🔹 Obtener respuesta específica (usuario + pregunta)
        public async Task<RespuestaUsuarioQuiz?> GetByUsuarioPreguntaAsync(int usuarioId, int preguntaId)
        {
            return await _context.RespuestaUsuarioQuizzes
                .FirstOrDefaultAsync(r => r.IdUsuario == usuarioId && r.IdPregunta == preguntaId);
        }

        // 🔹 Crear respuesta
        public async Task<RespuestaUsuarioQuiz> CreateAsync(RespuestaUsuarioQuiz respuesta)
        {
            // 🔥 Verificar si ya respondió
            var existe = await _context.RespuestaUsuarioQuizzes
                .AnyAsync(r => r.IdUsuario == respuesta.IdUsuario && r.IdPregunta == respuesta.IdPregunta);

            if (existe)
                throw new Exception("El usuario ya respondió esta pregunta");

            // 🔥 Obtener opción correcta
            var opcion = await _context.OpcionRespuestas
                .FirstOrDefaultAsync(o => o.IdOpcion == respuesta.IdOpcionElegida);

            if (opcion == null)
                throw new Exception("Opción no válida");

            // 🔥 Evaluar automáticamente
            respuesta.Correcta = opcion.EsCorrecta;
            respuesta.FechaRespuesta = DateTime.UtcNow;

            await _context.RespuestaUsuarioQuizzes.AddAsync(respuesta);
            await _context.SaveChangesAsync();
            return respuesta;
        }

        // 🔹 Actualizar respuesta (poco común, pero posible)
        public async Task<bool> UpdateAsync(RespuestaUsuarioQuiz respuesta)
        {
            var existing = await _context.RespuestaUsuarioQuizzes
                .FirstOrDefaultAsync(r => r.IdRespuesta == respuesta.IdRespuesta);

            if (existing == null)
                return false;

            var opcion = await _context.OpcionRespuestas
                .FirstOrDefaultAsync(o => o.IdOpcion == respuesta.IdOpcionElegida);

            if (opcion == null)
                throw new Exception("Opción no válida");

            existing.IdOpcionElegida = respuesta.IdOpcionElegida;
            existing.Correcta = opcion.EsCorrecta;
            existing.FechaRespuesta = DateTime.UtcNow;

            _context.RespuestaUsuarioQuizzes.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Eliminar respuesta
        public async Task<bool> DeleteAsync(int id)
        {
            var respuesta = await _context.RespuestaUsuarioQuizzes
                .FirstOrDefaultAsync(r => r.IdRespuesta == id);

            if (respuesta == null)
                return false;

            _context.RespuestaUsuarioQuizzes.Remove(respuesta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
