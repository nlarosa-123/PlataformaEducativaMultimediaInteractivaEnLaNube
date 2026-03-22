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

        public async Task<int> CreateAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            return quiz.IdQuiz;
        }

        public async Task<Quiz> GetByIdAsync(int id)
        {
            return await _context.Quizzes
                .Include(q => q.Preguntas)
                .FirstOrDefaultAsync(q => q.IdQuiz == id);
        }

        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await _context.Quizzes
                .Include(q => q.Preguntas)
                .ToListAsync();
        }

        public async Task UpdateAsync(Quiz quiz)
        {
            _context.Quizzes.Update(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
            }
        }
    }
}
