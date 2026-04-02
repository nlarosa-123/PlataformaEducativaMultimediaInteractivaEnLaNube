using BackendParaPlataforma.Entities;

public interface IAnalisisIARepository {

    Task<AnalisisIA> GuardarAnalisis(AnalisisIA analisis);
    Task<AnalisisIA?> ObtenerAnalisisPorDiario(int idDiario);
    Task<bool> ActualizarAnalisis(AnalisisIA analisis);
    Task<bool> EliminarAnalisis(int id);
}