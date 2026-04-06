using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IPreguntaQuizRepository
    {
        Task<IEnumerable<PreguntaQuiz>> GetAllAsync();
        Task<PreguntaQuiz?> GetByIdAsync(int id);

        Task<IEnumerable<PreguntaQuiz>> GetByQuizIdAsync(int quizId);

        Task<PreguntaQuiz> CreateAsync(PreguntaQuiz pregunta);
        Task<bool> UpdateAsync(PreguntaQuiz pregunta);
        Task<bool> DeleteAsync(int id);
    }
}
