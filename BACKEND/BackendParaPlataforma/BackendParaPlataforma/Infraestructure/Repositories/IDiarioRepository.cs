using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IDiarioEmocionalRepository
    {
        Task<List<DiarioEmocional>> GetAllAsync();
        Task<DiarioEmocional?> GetByIdAsync(int id);

        Task<List<DiarioEmocional>> GetByUsuarioAsync(int usuarioId);
        Task<List<DiarioEmocional>> GetByFechaAsync(int usuarioId, DateTime fecha);

        Task<DiarioEmocional> CreateAsync(DiarioEmocional diario);
        Task<bool> UpdateAsync(DiarioEmocional diario);
        Task<bool> DeleteAsync(int id);

        // ?? Extras clave para tu app
        Task<DiarioEmocional?> GetLatestByUsuarioAsync(int usuarioId);
    }
}