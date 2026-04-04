using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IEstadisticaUsuarioRepository
    {
        Task<List<EstadisticaUsuario>> GetAllAsync();
        Task<EstadisticaUsuario?> GetByIdAsync(int id);
        Task<EstadisticaUsuario?> GetByUsuarioIdAsync(int usuarioId);

        Task<EstadisticaUsuario> CreateAsync(EstadisticaUsuario estadistica);
        Task<bool> UpdateAsync(EstadisticaUsuario estadistica);
        Task<bool> DeleteAsync(int id);

        // ?? Método clave
        Task<bool> UpsertAsync(EstadisticaUsuario estadistica);
    }
}