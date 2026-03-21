using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IEstadisticasRepository
    {
        Task<List<Estadisticas>> GetAllAsync();

        Task<Estadisticas?> GetByIdAsync(int id);

        Task AddAsync(Estadisticas stats);

        Task DeleteAsync(Estadisticas stats);

        Task SaveChangesAsync();
    }
}
