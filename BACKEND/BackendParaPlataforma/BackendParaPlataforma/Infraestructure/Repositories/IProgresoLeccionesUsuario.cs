using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IProgresoLeccionesRepository
    {
        Task<List<ProgresoLeccionesUsuario>> GetAllAsync();

        Task<ProgresoLeccionesUsuario?> GetByIdAsync(int id); 

        //Devuelve el progreso de todas las lecciones de un usuario
        Task<List<ProgresoLeccionesUsuario>> GetByIdUserAsync(int idUser);

        Task AddAsync(ProgresoLeccionesUsuario progreso);

        Task DeleteAsync(ProgresoLeccionesUsuario progreso);

        Task SaveChangesAsync();
    }
}