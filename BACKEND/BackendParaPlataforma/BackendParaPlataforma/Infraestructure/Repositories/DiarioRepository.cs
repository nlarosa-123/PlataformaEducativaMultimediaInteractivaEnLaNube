using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class DiarioEmocionalRepository : IDiarioEmocionalRepository
    {
        private readonly AppDbContext _context;

        public DiarioEmocionalRepository(AppDbContext context)
        {
            _context = context;
        }

        // ?? Obtener todos
        public async Task<List<DiarioEmocional>> GetAllAsync()
        {
            return await _context.DiariosEmocionales
                .Include(d => d.Emocion)
                .Include(d => d.AnalisisIA)
                .Include(d => d.ReflexionMejora)
                .AsNoTracking()
                .ToListAsync();
        }

        // ?? Obtener por ID
        public async Task<DiarioEmocional?> GetByIdAsync(int id)
        {
            return await _context.DiariosEmocionales
                .Include(d => d.Emocion)
                .Include(d => d.AnalisisIA)
                .Include(d => d.ReflexionMejora)
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(d => d.Id_Diario == id);
        }

        // ?? Obtener diarios por usuario
        public async Task<List<DiarioListaDto>> GetByUsuarioAsync(int usuarioId)
        {
            return await _context.DiariosEmocionales
                .Where(d => d.Id_Usuario == usuarioId)
                .OrderByDescending(d => d.Fecha)
                .Select(d => new DiarioListaDto
                {
                    Id_Diario = d.Id_Diario,
                    Fecha = d.Fecha,
                    Texto_Usuario = d.Texto_Usuario,
                    NombreEmocion = d.Emocion.Nombre,
                    Emoji = d.Emocion.Emoji
                })
                .ToListAsync();
        }

        // ?? Obtener diarios por fecha (ej: un día específico)
        public async Task<List<DiarioEmocional>> GetByFechaAsync(int usuarioId, DateTime fecha)
        {
            return await _context.DiariosEmocionales
                .Where(d => d.Id_Usuario == usuarioId && d.Fecha.Date == fecha.Date)
                .Include(d => d.Emocion)
                .Include(d => d.AnalisisIA)
                .ToListAsync();
        }

        // ?? Crear
        public async Task<DiarioEmocional> CreateAsync(DiarioEmocional diario)
        {
            diario.Fecha_Creacion = DateTime.UtcNow;

            await _context.DiariosEmocionales.AddAsync(diario);
            await _context.SaveChangesAsync();

            return diario;
        }

        // ?? Actualizar
        public async Task<bool> UpdateAsync(DiarioEmocional diario)
        {
            var existing = await _context.DiariosEmocionales
                .FirstOrDefaultAsync(d => d.Id_Diario == diario.Id_Diario);

            if (existing == null)
                return false;

            existing.Texto_Usuario = diario.Texto_Usuario;
            existing.Audio_Url = diario.Audio_Url;
            existing.Id_Emocion_Usuario = diario.Id_Emocion_Usuario;
            existing.Fecha = diario.Fecha;

            await _context.SaveChangesAsync();
            return true;
        }

        // ?? Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            var diario = await _context.DiariosEmocionales.FindAsync(id);

            if (diario == null)
                return false;

            _context.DiariosEmocionales.Remove(diario);
            await _context.SaveChangesAsync();

            return true;
        }

        // ?? Obtener último diario del usuario (clave para dashboard)
        public async Task<DiarioEmocionalDto?> GetLatestByUsuarioAsync(int usuarioId)
        {
            return await _context.DiariosEmocionales
                .Where(d => d.Id_Usuario == usuarioId)
                .OrderByDescending(d => d.Fecha)
                .Select(d => new DiarioEmocionalDto
                {
                    Id_Diario = d.Id_Diario,
                    Id_Usuario = d.Id_Usuario,
                    Fecha = d.Fecha,
                    Texto_Usuario = d.Texto_Usuario,

                    NombreEmocion = d.Emocion.Nombre,
                    Emoji = d.Emocion.Emoji,

                    // ? ya no lista
                    Tono = d.AnalisisIA.Tono_Detectado,

                    // ? ya no lista
                    Reflexion = d.ReflexionMejora.Texto_Reflexion
                })
                .FirstOrDefaultAsync();
        }
        public async Task<DiarioEmocionalDto?> GetHoyByUsuarioAsync(int usuarioId)
        {
            var hoy = DateTime.UtcNow.Date;

            return await _context.DiariosEmocionales
                .Where(d => d.Id_Usuario == usuarioId
                         && d.Fecha.Date == hoy)
                .Select(d => new DiarioEmocionalDto
                {
                    Id_Diario = d.Id_Diario,
                    Id_Usuario = d.Id_Usuario,
                    Fecha = d.Fecha,
                    Texto_Usuario = d.Texto_Usuario,

                    NombreEmocion = d.Emocion.Nombre,
                    Emoji = d.Emocion.Emoji,

                    Tono = d.AnalisisIA != null
                        ? d.AnalisisIA.Tono_Detectado
                        : null,

                    Reflexion = d.ReflexionMejora != null
                        ? d.ReflexionMejora.Texto_Reflexion
                        : null
                })
                .FirstOrDefaultAsync();
        }
    }
}