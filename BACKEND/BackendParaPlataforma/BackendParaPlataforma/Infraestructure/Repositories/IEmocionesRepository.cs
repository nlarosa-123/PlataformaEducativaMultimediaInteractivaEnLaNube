using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IEmocionesRepository
    {
        Task<List<Emociones>> GetAllAsync();

        Task<Emociones?> GetByIdAsync(int id);

        Task AddAsync(Emociones emocion);

        Task DeleteAsync(Emociones emocion);

        Task SaveChangesAsync();
    }
}
