using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class SentimentResultRepository : ISentimentResultRepository
    {
        private readonly AppDbContext _context;

        public SentimentResultRepository(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos
        public async Task<List<SentimentResult>> GetAllAsync()
        {
            return await _context.Set<SentimentResult>()
                .Include(s => s.DiarioEmocional)
                .ToListAsync();
        }

        // 🔹 Obtener por Id
        public async Task<SentimentResult?> GetByIdAsync(int id)
        {
            return await _context.Set<SentimentResult>()
                .Include(s => s.DiarioEmocional)
                .FirstOrDefaultAsync(s => s.Id_Analisis == id);
        }

        // 🔹 Obtener por Diario
        public async Task<List<SentimentResult>> GetByDiarioIdAsync(int diarioId)
        {
            return await _context.Set<SentimentResult>()
                .Where(s => s.Id_Diario == diarioId)
                .Include(s => s.DiarioEmocional)
                .OrderByDescending(s => s.Fecha_Analisis)
                .ToListAsync();
        }

        // 🔹 Crear
        public async Task<SentimentResult> CreateAsync(SentimentResult sentiment)
        {
            sentiment.Fecha_Analisis = DateTime.UtcNow;

            await _context.Set<SentimentResult>().AddAsync(sentiment);
            await _context.SaveChangesAsync();

            return sentiment;
        }

        // 🔹 Actualizar
        public async Task<bool> UpdateAsync(SentimentResult sentiment)
        {
            var existing = await _context.Set<SentimentResult>()
                .FirstOrDefaultAsync(s => s.Id_Analisis == sentiment.Id_Analisis);

            if (existing == null)
                return false;

            existing.Provider = sentiment.Provider;
            existing.Sentiment = sentiment.Sentiment;
            existing.Coincide_Usuario = sentiment.Coincide_Usuario;

            existing.Positive = sentiment.Positive;
            existing.Neutral = sentiment.Neutral;
            existing.Negative = sentiment.Negative;

            existing.Score = sentiment.Score;
            existing.Magnitude = sentiment.Magnitude;

            existing.Confidence = sentiment.Confidence;
            existing.Explanation = sentiment.Explanation;
            existing.RawJson = sentiment.RawJson;

            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            var sentiment = await _context.Set<SentimentResult>().FindAsync(id);

            if (sentiment == null)
                return false;

            _context.Set<SentimentResult>().Remove(sentiment);
            await _context.SaveChangesAsync();

            return true;
        }

        // 🔹 Obtener el más reciente por Diario
        public async Task<SentimentResult?> GetLatestByDiarioAsync(int diarioId)
        {
            return await _context.Set<SentimentResult>()
                .Where(s => s.Id_Diario == diarioId)
                .OrderByDescending(s => s.Fecha_Analisis)
                .FirstOrDefaultAsync();
        }
        public async Task<SentimentResult?> GetByDiarioAndProviderAsync(int diarioId, string provider)
        {
            return await _context.Set<SentimentResult>()
                .FirstOrDefaultAsync(s => s.Id_Diario == diarioId && s.Provider == provider);
        }
        public async Task<SentimentResult> UpsertAsync(SentimentResult sentiment)
        {
            var existing = await _context.Set<SentimentResult>()
                .FirstOrDefaultAsync(s =>
                    s.Id_Diario == sentiment.Id_Diario &&
                    s.Provider == sentiment.Provider);

            if (existing == null)
            {
                sentiment.Fecha_Analisis = DateTime.UtcNow;
                await _context.AddAsync(sentiment);
            }
            else
            {
                existing.Sentiment = sentiment.Sentiment;
                existing.Positive = sentiment.Positive;
                existing.Neutral = sentiment.Neutral;
                existing.Negative = sentiment.Negative;
                existing.Score = sentiment.Score;
                existing.Magnitude = sentiment.Magnitude;
                existing.Confidence = sentiment.Confidence;
                existing.Explanation = sentiment.Explanation;
                existing.RawJson = sentiment.RawJson;
                existing.Coincide_Usuario = sentiment.Coincide_Usuario;
            }

            await _context.SaveChangesAsync();
            return sentiment;
        }
    }
}
