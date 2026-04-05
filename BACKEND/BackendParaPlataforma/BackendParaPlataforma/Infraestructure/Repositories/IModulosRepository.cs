using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IModulosRepository
    {
        Task<IEnumerable<ModuloDto>> GetAllAsync();
        Task<Modulos?> GetByIdAsync(int id);
        Task<Modulos> CreateAsync(Modulos modulo);
        Task<bool> UpdateAsync(Modulos modulo);
        Task<bool> DeleteAsync(int id);
    }
}
