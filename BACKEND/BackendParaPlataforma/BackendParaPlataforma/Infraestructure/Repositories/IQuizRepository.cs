using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAllAsync();
        Task<Quiz?> GetByIdAsync(int id);
        Task<Quiz?> GetByLeccionIdAsync(int leccionId);

        Task<Quiz> CreateAsync(Quiz quiz);
        Task<bool> UpdateAsync(Quiz quiz);
        Task<bool> DeleteAsync(int id);
    }
}
