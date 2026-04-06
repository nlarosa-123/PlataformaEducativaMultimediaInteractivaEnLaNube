using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IProgresoUsuarioRepository
    {
        Task<IEnumerable<ProgresoUsuario>> GetAllAsync();
        Task<ProgresoUsuario?> GetByIdAsync(int id);
        Task<IEnumerable<ProgresoUsuario>> GetByUsuarioIdAsync(int usuarioId);

        Task<ProgresoUsuario> CreateAsync(ProgresoUsuario progreso);
        Task<bool> UpdateAsync(ProgresoUsuario progreso);
        Task<bool> DeleteAsync(int id);
    }

}
