using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IReflexionMejoraRepository
    {
        Task<List<ReflexionMejora>> GetAllAsync();
        Task<ReflexionMejora?> GetByIdAsync(int id);
        Task<ReflexionMejora?> GetByDiarioIdAsync(int diarioId);

        Task<ReflexionMejora> CreateAsync(ReflexionMejora reflexion);
        Task<bool> UpdateAsync(ReflexionMejora reflexion);
        Task<bool> DeleteAsync(int id);
    }
}