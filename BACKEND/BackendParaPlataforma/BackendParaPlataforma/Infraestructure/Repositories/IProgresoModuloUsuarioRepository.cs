using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IProgresoModuloUsuarioRepository
    {
        Task<IEnumerable<ProgresoModuloUsuario>> GetAllAsync();
        Task<ProgresoModuloUsuario?> GetByIdAsync(int id);
        Task<IEnumerable<ProgresoModuloUsuarioDto>> GetByUsuarioIdAsync(int usuarioId);
        Task<ProgresoModuloUsuario?> GetByUsuarioModuloAsync(int usuarioId, int moduloId);

        Task<ProgresoModuloUsuario> CreateAsync(ProgresoModuloUsuario progreso);
        Task<bool> UpdateAsync(ProgresoModuloUsuario progreso);
        Task<bool> DeleteAsync(int id);
    }
}