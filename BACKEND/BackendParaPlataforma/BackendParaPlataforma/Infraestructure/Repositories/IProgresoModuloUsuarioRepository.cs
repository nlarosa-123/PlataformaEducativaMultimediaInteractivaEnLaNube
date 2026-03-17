using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IProgresoModuloUsuarioRepository
    {
        Task<List<ProgresoModuloUsuario>> ObtenerTodosAsync();

        Task<ProgresoModuloUsuario?> ObtenerPorIdAsync(int id);

        Task<ProgresoModuloUsuario?> ObtenerAsync(int idUsuario, int idModulo);

        Task<ProgresoModuloUsuario> CrearAsync(ProgresoModuloUsuario progreso);

        void Actualizar(ProgresoModuloUsuario progreso);

        void Eliminar(ProgresoModuloUsuario progreso);

        Task SaveChangesAsync();
        #region usuario
        Task<List<ProgresoModuloUsuario>> ObtenerPorUsuarioAsync(int idUsuario);
        #endregion
    }
}