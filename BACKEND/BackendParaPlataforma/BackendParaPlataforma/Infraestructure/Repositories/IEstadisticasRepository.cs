using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IEstadisticaUsuarioRepository
    {
        // ?? Obtener todas
        Task<List<EstadisticaUsuario>> GetAllAsync();

        // ?? Obtener por ID
        Task<EstadisticaUsuario?> GetByIdAsync(int id);

        // ?? Obtener TODAS las estadísticas de un usuario (1:N)
        Task<List<EstadisticaUsuario>> GetByUsuarioIdAsync(int usuarioId);

        // ?? Obtener por usuario + provider
        Task<EstadisticaUsuario?> GetByUsuarioAndProviderAsync(int usuarioId, string provider);

        // ?? Crear
        Task<EstadisticaUsuario> CreateAsync(EstadisticaUsuario estadistica);

        // ?? Actualizar
        Task<bool> UpdateAsync(EstadisticaUsuario estadistica);

        // ?? Eliminar
        Task<bool> DeleteAsync(int id);

        // ?? UPSERT (crear o actualizar por provider)
        Task<bool> UpsertAsync(EstadisticaUsuario estadistica);
    }
}