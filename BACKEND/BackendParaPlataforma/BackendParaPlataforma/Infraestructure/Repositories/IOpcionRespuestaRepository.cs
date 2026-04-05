using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IOpcionRespuestaRepository
    {
        Task<IEnumerable<OpcionRespuesta>> GetAllAsync();
        Task<OpcionRespuesta?> GetByIdAsync(int id);

        Task<IEnumerable<OpcionRespuesta>> GetByPreguntaIdAsync(int preguntaId);
        Task<OpcionRespuesta?> GetCorrectaByPreguntaIdAsync(int preguntaId);

        Task<OpcionRespuesta> CreateAsync(OpcionRespuesta opcion);
        Task<bool> UpdateAsync(OpcionRespuesta opcion);
        Task<bool> DeleteAsync(int id);
    }
}
