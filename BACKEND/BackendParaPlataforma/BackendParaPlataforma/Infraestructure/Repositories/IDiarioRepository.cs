using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IDiarioRepository
    {
        Task AddAsync(DiarioEmocional diarioEmocional);
        Task<List<DiarioEmocional>> GetByUsuarioIdAsync(int usuarioId);
        Task SaveChangesAsync();
    }
}
