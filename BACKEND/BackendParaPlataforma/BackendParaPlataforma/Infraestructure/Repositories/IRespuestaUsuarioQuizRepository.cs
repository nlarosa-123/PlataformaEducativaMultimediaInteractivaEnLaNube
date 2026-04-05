using BackendParaPlataforma.Entities;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IRespuestaUsuarioQuizRepository
    {
        Task<IEnumerable<RespuestaUsuarioQuiz>> GetAllAsync();
        Task<RespuestaUsuarioQuiz?> GetByIdAsync(int id);

        Task<IEnumerable<RespuestaUsuarioQuiz>> GetByUsuarioAsync(int usuarioId);
        Task<IEnumerable<RespuestaUsuarioQuiz>> GetByPreguntaAsync(int preguntaId);

        Task<RespuestaUsuarioQuiz?> GetByUsuarioPreguntaAsync(int usuarioId, int preguntaId);

        Task<RespuestaUsuarioQuiz> CreateAsync(RespuestaUsuarioQuiz respuesta);
        Task<bool> UpdateAsync(RespuestaUsuarioQuiz respuesta);
        Task<bool> DeleteAsync(int id);
    }
}
