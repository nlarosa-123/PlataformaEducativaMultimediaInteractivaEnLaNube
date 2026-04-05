using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
	public interface IModulosRepository
	{
		Task<List<Modulos>> GetAllAsync();

		Task<Modulos?> GetByIdAsync(int id);

		Task<Modulos?> GetByTituloAsync(string titulo);

		Task AddAsync(Modulos mod);

		Task DeleteAsync(Modulos mod);

		Task SaveChangesAsync();
	}
}