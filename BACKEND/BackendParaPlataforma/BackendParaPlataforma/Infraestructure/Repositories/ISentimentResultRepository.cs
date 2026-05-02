using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface ISentimentResultRepository
    {
        // 🔹 Obtener todos
        Task<List<SentimentResult>> GetAllAsync();

        // 🔹 Obtener por Id
        Task<SentimentResult?> GetByIdAsync(int id);

        // 🔹 Obtener todos los análisis de un diario (1:N)
        Task<List<SentimentResult>> GetByDiarioIdAsync(int diarioId);

        // 🔹 Obtener el más reciente por diario
        Task<SentimentResult?> GetLatestByDiarioAsync(int diarioId);

        // 🔹 Obtener por diario + provider
        Task<SentimentResult?> GetByDiarioAndProviderAsync(int diarioId, string provider);

        // 🔹 Crear
        Task<SentimentResult> CreateAsync(SentimentResult sentiment);

        // 🔹 Upsert (crear o actualizar por provider)
        Task<SentimentResult> UpsertAsync(SentimentResult sentiment);

        // 🔹 Actualizar
        Task<bool> UpdateAsync(SentimentResult sentiment);

        // 🔹 Eliminar
        Task<bool> DeleteAsync(int id);
    }
}