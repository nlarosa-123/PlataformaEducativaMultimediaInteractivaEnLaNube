using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IEmocionesRepository
    {
        Task<List<Emociones>> GetAllAsync();
        Task<Emociones?> GetByIdAsync(int id);

        Task<Emociones> CreateAsync(Emociones emocion);
        Task<bool> UpdateAsync(Emociones emocion);
        Task<bool> DeleteAsync(int id);

        // 🔥 Extras útiles
        Task<List<Emociones>> GetByRangoValorAsync(decimal min, decimal max);
        Task<Emociones?> GetByNombreAsync(string nombre);
    }
}