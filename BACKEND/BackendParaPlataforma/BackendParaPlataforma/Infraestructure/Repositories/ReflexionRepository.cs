using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class ReflexionMejoraRepository : IReflexionMejoraRepository
    {
        private readonly AppDbContext _context;

        public ReflexionMejoraRepository(AppDbContext context)
        {
            _context = context;
        }

        // ?? Obtener todas
        public async Task<List<ReflexionMejora>> GetAllAsync()
        {
            return await _context.ReflexionesMejora
                .Include(r => r.DiarioEmocional)
                .AsNoTracking()
                .ToListAsync();
        }

        // ?? Obtener por ID
        public async Task<ReflexionMejora?> GetByIdAsync(int id)
        {
            return await _context.ReflexionesMejora
                .Include(r => r.DiarioEmocional)
                .FirstOrDefaultAsync(r => r.Id_Reflexion == id);
        }

        // ?? Obtener reflexión por diario (clave en tu app)
        public async Task<ReflexionMejora?> GetByDiarioIdAsync(int diarioId)
        {
            return await _context.ReflexionesMejora
                .Where(r => r.Id_Diario == diarioId)
                .OrderByDescending(r => r.Fecha_Creacion)
                .FirstOrDefaultAsync();
        }

        // ?? Crear
        public async Task<ReflexionMejora> CreateAsync(ReflexionMejora reflexion)
        {
            reflexion.Fecha_Creacion = DateTime.UtcNow;

            await _context.ReflexionesMejora.AddAsync(reflexion);
            await _context.SaveChangesAsync();

            return reflexion;
        }

        // ?? Actualizar
        public async Task<bool> UpdateAsync(ReflexionMejora reflexion)
        {
            var existing = await _context.ReflexionesMejora
                .FirstOrDefaultAsync(r => r.Id_Reflexion == reflexion.Id_Reflexion);

            if (existing == null)
                return false;

            existing.Texto_Reflexion = reflexion.Texto_Reflexion;

            await _context.SaveChangesAsync();
            return true;
        }

        // ?? Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            var reflexion = await _context.ReflexionesMejora.FindAsync(id);

            if (reflexion == null)
                return false;

            _context.ReflexionesMejora.Remove(reflexion);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}