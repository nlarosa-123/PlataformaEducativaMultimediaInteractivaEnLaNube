using BackendParaPlataforma.Dtos;
using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IAnalisisIARepository
    {
        Task<List<AnalisisIA>> GetAllAsync();
        Task<AnalisisIA?> GetByIdAsync(int id);
        Task<List<AnalisisIADto>> GetByDiarioIdAsync(int diarioId);

        Task<AnalisisIA> CreateAsync(AnalisisIA analisis);
        Task<bool> UpdateAsync(AnalisisIA analisis);
        Task<bool> DeleteAsync(int id);
        Task<AnalisisIA?> GetLatestByDiarioAsync(int diarioId);
    }
}