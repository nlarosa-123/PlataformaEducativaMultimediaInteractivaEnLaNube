using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IEstadisticasRepository
    {
        Task<List<EstadisticaUsuario>> GetAllAsync();

        Task<EstadisticaUsuario?> GetByIdAsync(int id);

        Task AddAsync(EstadisticaUsuario stats);

        Task DeleteAsync(EstadisticaUsuario stats);

        Task SaveChangesAsync();
    }
}
