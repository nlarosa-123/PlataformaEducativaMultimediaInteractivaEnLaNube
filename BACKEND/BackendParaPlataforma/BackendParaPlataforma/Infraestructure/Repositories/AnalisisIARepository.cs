using BackendParaPlataforma.Dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class AnalisisIARepository : IAnalisisIARepository
    {
        private readonly AppDbContext _context;

        public AnalisisIARepository(AppDbContext context)
        {
            _context = context;
        }

        // ?? Obtener todos
        public async Task<List<AnalisisIA>> GetAllAsync()
        {
            return await _context.AnalisisIA
                .Include(a => a.DiarioEmocional)
                .Include(a => a.Emociones)
                .ToListAsync();
        }

        // ?? Obtener por Id
        public async Task<AnalisisIA?> GetByIdAsync(int id)
        {
            return await _context.AnalisisIA
                .Include(a => a.DiarioEmocional)
                .Include(a => a.Emociones)
                .FirstOrDefaultAsync(a => a.Id_Analisis == id);
        }

        // ?? Obtener por Diario
        public async Task<List<AnalisisIADto>> GetByDiarioIdAsync(int diarioId)
        {
            return await _context.AnalisisIA
                .Where(a => a.Id_Diario == diarioId)
                .Include(a => a.Emociones)
                .Select(a => new AnalisisIADto
                {
                    Id_Analisis = a.Id_Analisis,
                    Id_Diario = a.Id_Diario,
                    Emocion_Detectada_IA = a.Emocion_Detectada_IA,
                    Tono_Detectado = a.Tono_Detectado,
                    Confianza = a.Confianza,
                    NombreEmocion = a.Emociones.Nombre,
                    Emoji = a.Emociones.Emoji
                })
                .ToListAsync();
        }

        // ?? Crear
        public async Task<AnalisisIA> CreateAsync(AnalisisIA analisis)
        {
            analisis.Fecha_Analisis = DateTime.UtcNow;

            await _context.AnalisisIA.AddAsync(analisis);
            await _context.SaveChangesAsync();

            return analisis;
        }

        // ?? Actualizar
        public async Task<bool> UpdateAsync(AnalisisIA analisis)
        {
            var existing = await _context.AnalisisIA
                .FirstOrDefaultAsync(a => a.Id_Analisis == analisis.Id_Analisis);

            if (existing == null)
                return false;

            existing.Emocion_Detectada_IA = analisis.Emocion_Detectada_IA;
            existing.Tono_Detectado = analisis.Tono_Detectado;
            existing.Confianza = analisis.Confianza;
            existing.Coincide_Usuario = analisis.Coincide_Usuario;

            await _context.SaveChangesAsync();
            return true;
        }

        // ?? Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            var analisis = await _context.AnalisisIA.FindAsync(id);

            if (analisis == null)
                return false;

            _context.AnalisisIA.Remove(analisis);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AnalisisIA?> GetLatestByDiarioAsync(int diarioId)
        {
            return await _context.AnalisisIA
                .Where(a => a.Id_Diario == diarioId)
                .OrderByDescending(a => a.Fecha_Analisis)
                .FirstOrDefaultAsync();
        }
    }
}