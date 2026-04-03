using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class EstadisticaUsuarioRepository : IEstadisticaUsuarioRepository
    {
        private readonly AppDbContext _context;

        public EstadisticaUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        // ?? Obtener todas
        public async Task<List<EstadisticaUsuario>> GetAllAsync()
        {
            return await _context.EstadisticaUsuario
                .Include(e => e.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        // ?? Obtener por ID
        public async Task<EstadisticaUsuario?> GetByIdAsync(int id)
        {
            return await _context.EstadisticaUsuario
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(e => e.IdEstadistica == id);
        }

        // ?? Obtener por usuario (el más importante)
        public async Task<EstadisticaUsuario?> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.EstadisticaUsuario
                .FirstOrDefaultAsync(e => e.IdUsuario == usuarioId);
        }

        // ?? Crear
        public async Task<EstadisticaUsuario> CreateAsync(EstadisticaUsuario estadistica)
        {
            estadistica.UltimaActualizacion = DateTime.UtcNow;

            await _context.EstadisticaUsuario.AddAsync(estadistica);
            await _context.SaveChangesAsync();

            return estadistica;
        }

        // ?? Actualizar
        public async Task<bool> UpdateAsync(EstadisticaUsuario estadistica)
        {
            var existing = await _context.EstadisticaUsuario
                .FirstOrDefaultAsync(e => e.IdEstadistica == estadistica.IdEstadistica);

            if (existing == null)
                return false;

            existing.PorcentajeCoincidenciaIA = estadistica.PorcentajeCoincidenciaIA;
            existing.EmocionMasFrecuente = estadistica.EmocionMasFrecuente;
            existing.RachaDiasRegistrados = estadistica.RachaDiasRegistrados;
            existing.UltimaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        // ?? Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            var estadistica = await _context.EstadisticaUsuario.FindAsync(id);

            if (estadistica == null)
                return false;

            _context.EstadisticaUsuario.Remove(estadistica);
            await _context.SaveChangesAsync();

            return true;
        }

        // ?? UPSERT (crear o actualizar automáticamente)
        public async Task<bool> UpsertAsync(EstadisticaUsuario estadistica)
        {
            var existing = await _context.EstadisticaUsuario
                .FirstOrDefaultAsync(e => e.IdUsuario == estadistica.IdUsuario);

            if (existing == null)
            {
                estadistica.UltimaActualizacion = DateTime.UtcNow;
                await _context.EstadisticaUsuario.AddAsync(estadistica);
            }
            else
            {
                existing.PorcentajeCoincidenciaIA = estadistica.PorcentajeCoincidenciaIA;
                existing.EmocionMasFrecuente = estadistica.EmocionMasFrecuente;
                existing.RachaDiasRegistrados = estadistica.RachaDiasRegistrados;
                existing.UltimaActualizacion = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}