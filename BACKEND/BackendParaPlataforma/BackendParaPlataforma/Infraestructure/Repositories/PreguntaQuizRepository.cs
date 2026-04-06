using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class PreguntaQuizRepository : IPreguntaQuizRepository
    {
        private readonly AppDbContext _context;

        public PreguntaQuizRepository(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todas las preguntas
        public async Task<IEnumerable<PreguntaQuiz>> GetAllAsync()
        {
            return await _context.PreguntasQuiz
                .Include(p => p.Quiz)
                .Include(p => p.Opciones)
                .ToListAsync();
        }

        // 🔹 Obtener por ID
        public async Task<PreguntaQuiz?> GetByIdAsync(int id)
        {
            return await _context.PreguntasQuiz
                .Include(p => p.Quiz)
                .Include(p => p.Opciones)
                .Include(p => p.respuestaUsuarioQuizzes)
                .FirstOrDefaultAsync(p => p.IdPregunta == id);
        }

        // 🔹 Obtener preguntas por quiz (ordenadas)
        public async Task<IEnumerable<PreguntaQuiz>> GetByQuizIdAsync(int quizId)
        {
            return await _context.PreguntasQuiz
                .Where(p => p.IdQuiz == quizId)
                .Include(p => p.Opciones)
                .OrderBy(p => p.Orden)
                .ToListAsync();
        }

        // 🔹 Crear pregunta
        public async Task<PreguntaQuiz> CreateAsync(PreguntaQuiz pregunta)
        {
            // 🔥 Auto-asignar orden si no viene
            if (pregunta.Orden == 0)
            {
                var ultimoOrden = await _context.PreguntasQuiz
                    .Where(p => p.IdQuiz == pregunta.IdQuiz)
                    .MaxAsync(p => (int?)p.Orden) ?? 0;

                pregunta.Orden = ultimoOrden + 1;
            }

            await _context.PreguntasQuiz.AddAsync(pregunta);
            await _context.SaveChangesAsync();
            return pregunta;
        }

        // 🔹 Actualizar pregunta
        public async Task<bool> UpdateAsync(PreguntaQuiz pregunta)
        {
            var existing = await _context.PreguntasQuiz
                .FirstOrDefaultAsync(p => p.IdPregunta == pregunta.IdPregunta);

            if (existing == null)
                return false;

            existing.Pregunta = pregunta.Pregunta;
            existing.Orden = pregunta.Orden;

            _context.PreguntasQuiz.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Eliminar pregunta
        public async Task<bool> DeleteAsync(int id)
        {
            var pregunta = await _context.PreguntasQuiz
                .FirstOrDefaultAsync(p => p.IdPregunta == id);

            if (pregunta == null)
                return false;

            _context.PreguntasQuiz.Remove(pregunta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
