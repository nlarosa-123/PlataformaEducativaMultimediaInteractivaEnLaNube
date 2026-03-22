namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IQuizRepository
    {
        Task<int> CreateAsync(Quiz quiz);
        Task<Quiz> GetByIdAsync(int id);
        Task<IEnumerable<Quiz>> GetAllAsync();
        Task UpdateAsync(Quiz quiz);
        Task DeleteAsync(int id);
    }
}
