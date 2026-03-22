namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public interface IPreguntaQuizRepository
    {

        Task<int> CreateAsync(PreguntaQuiz pregunta);
        Task<PreguntaQuiz> GetByIdAsync(int id);
        Task<IEnumerable<PreguntaQuiz>> GetByQuizIdAsync(int idQuiz);
        Task UpdateAsync(PreguntaQuiz pregunta);
        Task DeleteAsync(int id);
    }
}
