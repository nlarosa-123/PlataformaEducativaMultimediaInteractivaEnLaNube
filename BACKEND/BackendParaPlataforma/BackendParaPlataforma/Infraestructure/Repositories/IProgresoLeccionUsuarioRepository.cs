using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IProgresoLeccionUsuarioRepository
    {
        Task<IEnumerable<ProgresoLeccionUsuario>> GetAllAsync();
        Task<ProgresoLeccionUsuario?> GetByIdAsync(int id);

        Task<IEnumerable<ProgresoLeccionUsuario>> GetByUsuarioAsync(int usuarioId);
        Task<IEnumerable<ProgresoLeccionUsuario>> GetByLeccionAsync(int leccionId);

        Task<ProgresoLeccionUsuario?> GetByUsuarioLeccionAsync(int usuarioId, int leccionId);

        Task<ProgresoLeccionUsuario> CreateAsync(ProgresoLeccionUsuario progreso);
        Task<bool> UpdateAsync(ProgresoLeccionUsuario progreso);
        Task<bool> DeleteAsync(int id);
        Task<ProgresoLeccionUsuario> UpsertAsync(ProgresoLeccionUsuario progreso);
    }
}
