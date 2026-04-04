using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IDiarioEmocionalRepository
    {
        Task<List<DiarioEmocional>> GetAllAsync();
        Task<DiarioEmocional?> GetByIdAsync(int id);

        Task<List<DiarioListaDto>> GetByUsuarioAsync(int usuarioId);
        Task<List<DiarioEmocional>> GetByFechaAsync(int usuarioId, DateTime fecha);

        Task<DiarioEmocional> CreateAsync(DiarioEmocional diario);
        Task<bool> UpdateAsync(DiarioEmocional diario);
        Task<bool> DeleteAsync(int id);

        // ?? Extras clave para tu app
        Task<DiarioEmocionalDto?> GetLatestByUsuarioAsync(int usuarioId);
        Task<DiarioEmocionalDto?> GetHoyByUsuarioAsync(int usuarioId);
        Task<List<DiarioListaDto>> GetByUsuarioYFechaAsync(int usuarioId, DateTime fecha);
    }
}