using BackendParaPlataforma.Entities;

public interface IProgresoRepository {

    Task<ProgresoUsuario> GuardarProgreso(ProgresoUsuario progreso);
    Task<ProgresoUsuario?> ObtenerProgresoUsuario(int idUsuario, string modulo);
    Task<bool> ActualizarProgreso(ProgresoUsuario progreso);
    Task<bool> EliminarProgreso(int id);
}