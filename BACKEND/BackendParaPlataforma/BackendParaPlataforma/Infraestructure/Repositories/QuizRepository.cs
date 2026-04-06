using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los quizzes
        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await _context.Quizzes
                .Include(q => q.Lecciones)
                .Include(q => q.PreguntaQuizzes)
                .ToListAsync();
        }

        // 🔹 Obtener por ID
        public async Task<Quiz?> GetByIdAsync(int id)
        {
            return await _context.Quizzes
                .Include(q => q.Lecciones)
                .Include(q => q.PreguntaQuizzes)
                .FirstOrDefaultAsync(q => q.IdQuiz == id);
        }

        // 🔹 Obtener quiz por lección (1:1 normalmente)
        public async Task<Quiz?> GetByLeccionIdAsync(int leccionId)
        {
            return await _context.Quizzes
                .Include(q => q.PreguntaQuizzes)
                .FirstOrDefaultAsync(q => q.IdLeccion == leccionId);
        }

        // 🔹 Crear quiz
        public async Task<Quiz> CreateAsync(Quiz quiz)
        {
            // 🔥 Validar que no exista otro quiz para la misma lección
            var existe = await _context.Quizzes
                .AnyAsync(q => q.IdLeccion == quiz.IdLeccion);

            if (existe)
                throw new Exception("Ya existe un quiz para esta lección");

            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        // 🔹 Actualizar quiz
        public async Task<bool> UpdateAsync(Quiz quiz)
        {
            var existing = await _context.Quizzes
                .FirstOrDefaultAsync(q => q.IdQuiz == quiz.IdQuiz);

            if (existing == null)
                return false;

            existing.Titulo = quiz.Titulo;
            existing.Descripcion = quiz.Descripcion;

            _context.Quizzes.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Eliminar quiz
        public async Task<bool> DeleteAsync(int id)
        {
            var quiz = await _context.Quizzes
                .FirstOrDefaultAsync(q => q.IdQuiz == id);

            if (quiz == null)
                return false;

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
