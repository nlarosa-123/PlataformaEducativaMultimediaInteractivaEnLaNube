using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface ILeccionesRepository
    {
        Task<List<Lecciones>> GetAllAsync();

        Task<Lecciones?> GetByIdAsync(int id);

        Task AddAsync(Lecciones l);

        Task DeleteAsync(Lecciones l);

        Task SaveChangesAsync();
    }
}
