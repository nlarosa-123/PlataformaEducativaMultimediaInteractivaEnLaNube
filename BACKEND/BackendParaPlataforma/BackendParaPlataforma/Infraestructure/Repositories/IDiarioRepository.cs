using BackendParaPlataforma.Entities;

public interface IDiarioRepository {

    Task<DiarioEmocional> CrearDiario(DiarioEmocional diario);

    Task<List<DiarioEmocional>> ObtenerDiariosUsuario(int idUsuario);

    Task<DiarioEmocional?> ObtenerDiarioPorId(int idDiario);
}