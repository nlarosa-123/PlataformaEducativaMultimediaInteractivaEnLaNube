using BackendParaPlataforma.Infraestructure.Persistence;
using BackendParaPlataforma.Infraestructure.Repositories;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    //public class PreguntaQuizRepository : IPreguntaQuizRepository
    //{
    //    private readonly AppDbContext _context;

    //    public PreguntaQuizRepository(AppDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<int> CreateAsync(PreguntaQuiz pregunta)
    //    {
    //        await _context.PreguntasQuiz.AddAsync(pregunta);
    //        await _context.SaveChangesAsync();
    //        return pregunta.IdPregunta;
    //    }

    //    public async Task<PreguntaQuiz> GetByIdAsync(int id)
    //    {
    //        return await _context.PreguntasQuiz
    //            .Include(p => p.Opciones)
    //            .FirstOrDefaultAsync(p => p.IdPregunta == id);
    //    }

    //    public async Task<IEnumerable<PreguntaQuiz>> GetByQuizIdAsync(int idQuiz)
    //    {
    //        return await _context.PreguntasQuiz
    //            .Where(p => p.IdQuiz == idQuiz)
    //            .Include(p => p.Opciones)
    //            .ToListAsync();
    //    }

    //    public async Task UpdateAsync(PreguntaQuiz pregunta)
    //    {
    //        // Manejo simple: reemplaza opciones
    //        var existing = await _context.PreguntasQuiz
    //            .Include(p => p.Opciones)
    //            .FirstOrDefaultAsync(p => p.IdPregunta == pregunta.IdPregunta);

    //        if (existing != null)
    //        {
    //            existing.Pregunta = pregunta.Pregunta;
    //            existing.Orden = pregunta.Orden;

    //            // Reemplazar opciones
    //            _context.OpcionesRespuesta.RemoveRange(existing.Opciones);
    //            existing.Opciones = pregunta.Opciones;

    //            await _context.SaveChangesAsync();
    //        }
    //    }

    //    public async Task DeleteAsync(int id)
    //    {
    //        var pregunta = await _context.PreguntasQuiz.FindAsync(id);
    //        if (pregunta != null)
    //        {
    //            _context.PreguntasQuiz.Remove(pregunta);
    //            await _context.SaveChangesAsync();
    //        }
    //    }
    //    #region obtner quizzes
    //    public async Task<PreguntaQuiz> GetByIdWithQuizAsync(int id)
    //    {
    //        return await _context.PreguntasQuiz
    //            .Include(p => p.Quiz)      // <-- carga el Quiz asociado
    //            .Include(p => p.Opciones)  // <-- opcional: carga opciones
    //            .FirstOrDefaultAsync(p => p.IdPregunta == id);
    //    }
    //    public async Task<IEnumerable<PreguntaQuiz>> GetAllWithQuizAsync()
    //    {
    //        return await _context.PreguntasQuiz
    //            .Include(p => p.Quiz)
    //            .Include(p => p.Opciones)
    //            .ToListAsync();
    //    }
    //    #endregion 
    //}
}
