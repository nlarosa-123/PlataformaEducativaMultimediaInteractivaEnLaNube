using BackendParaPlataforma.Entities;

public interface IReflexionRepository {

    Task<ReflexionMejora> CrearReflexion(ReflexionMejora reflexion);
    Task<List<ReflexionMejora>> ObtenerReflexionesPorDiario(int idDiario);
    Task<bool> ActualizarReflexion(ReflexionMejora reflexion);
    Task<bool> EliminarReflexion(int id);
}