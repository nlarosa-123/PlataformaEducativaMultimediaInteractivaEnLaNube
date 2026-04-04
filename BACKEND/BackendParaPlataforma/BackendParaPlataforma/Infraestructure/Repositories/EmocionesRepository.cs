using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class EmocionesRepository : IEmocionesRepository
    {
        private readonly AppDbContext _context;

        public EmocionesRepository(AppDbContext context)
        {
            _context = context;
        }

        // 📌 Obtener todas
        public async Task<List<Emociones>> GetAllAsync()
        {
            return await _context.Emociones
                .AsNoTracking()
                .ToListAsync();
        }

        // 📌 Obtener por ID
        public async Task<Emociones?> GetByIdAsync(int id)
        {
            return await _context.Emociones
                .Include(e => e.DiariosEmocionales)
                .Include(e => e.AnalisisIAs)
                .FirstOrDefaultAsync(e => e.IdEmocion == id);
        }

        // 📌 Crear
        public async Task<Emociones> CreateAsync(Emociones emocion)
        {
            await _context.Emociones.AddAsync(emocion);
            await _context.SaveChangesAsync();
            return emocion;
        }

        // 📌 Actualizar
        public async Task<bool> UpdateAsync(Emociones emocion)
        {
            var existing = await _context.Emociones
                .FirstOrDefaultAsync(e => e.IdEmocion == emocion.IdEmocion);

            if (existing == null)
                return false;

            existing.Nombre = emocion.Nombre;
            existing.Emoji = emocion.Emoji;
            existing.Valor = emocion.Valor;

            await _context.SaveChangesAsync();
            return true;
        }

        // 📌 Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            var emocion = await _context.Emociones.FindAsync(id);

            if (emocion == null)
                return false;

            _context.Emociones.Remove(emocion);
            await _context.SaveChangesAsync();

            return true;
        }

        // 🔥 Obtener emociones por rango de valor (-2 a +2)
        public async Task<List<Emociones>> GetByRangoValorAsync(decimal min, decimal max)
        {
            return await _context.Emociones
                .Where(e => e.Valor.HasValue && e.Valor >= min && e.Valor <= max)
                .ToListAsync();
        }

        // 🔍 Obtener por nombre (útil para IA o validaciones)
        public async Task<Emociones?> GetByNombreAsync(string nombre)
        {
            return await _context.Emociones
                .FirstOrDefaultAsync(e => e.Nombre.ToLower() == nombre.ToLower());
        }
    }
}