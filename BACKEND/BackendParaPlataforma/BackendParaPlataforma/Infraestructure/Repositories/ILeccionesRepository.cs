using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface ILeccionesRepository
    {
        Task<IEnumerable<Lecciones>> GetAllAsync();
        Task<Lecciones?> GetByIdAsync(int id);
        Task<IEnumerable<Lecciones>> GetByModuloIdAsync(int moduloId);

        Task<Lecciones> CreateAsync(Lecciones leccion);
        Task<bool> UpdateAsync(Lecciones leccion);
        Task<bool> DeleteAsync(int id);
    }
}
