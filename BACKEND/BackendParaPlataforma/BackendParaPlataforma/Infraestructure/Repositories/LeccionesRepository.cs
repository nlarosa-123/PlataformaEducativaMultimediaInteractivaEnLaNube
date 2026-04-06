using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class LeccionesRepository : ILeccionesRepository
    {
        private readonly AppDbContext _context;

        public LeccionesRepository(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todas las lecciones
        public async Task<IEnumerable<Lecciones>> GetAllAsync()
        {
            return await _context.Lecciones
                .Include(l => l.Modulos)
                .Include(l => l.Quiz)
                .ToListAsync();
        }

        // 🔹 Obtener por ID
        public async Task<Lecciones?> GetByIdAsync(int id)
        {
            return await _context.Lecciones
                .Include(l => l.Modulos)
                .Include(l => l.Quiz)
                .Include(l => l.ProgresosUsuarios)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        // 🔹 Obtener lecciones por módulo (ordenadas)
        public async Task<IEnumerable<Lecciones>> GetByModuloIdAsync(int moduloId)
        {
            return await _context.Lecciones
                .Where(l => l.IdModulo == moduloId)
                .OrderBy(l => l.Orden)
                .ToListAsync();
        }

        // 🔹 Crear lección
        public async Task<Lecciones> CreateAsync(Lecciones leccion)
        {
            // 🔥 Auto-asignar orden si no viene definido
            if (leccion.Orden == 0)
            {
                var ultimoOrden = await _context.Lecciones
                    .Where(l => l.IdModulo == leccion.IdModulo)
                    .MaxAsync(l => (int?)l.Orden) ?? 0;

                leccion.Orden = ultimoOrden + 1;
            }

            await _context.Lecciones.AddAsync(leccion);
            await _context.SaveChangesAsync();
            return leccion;
        }

        // 🔹 Actualizar lección
        public async Task<bool> UpdateAsync(Lecciones leccion)
        {
            var existing = await _context.Lecciones.FindAsync(leccion.Id);

            if (existing == null)
                return false;

            existing.Titulo = leccion.Titulo;
            existing.TipoContenido = leccion.TipoContenido;
            existing.ContenidoTxt = leccion.ContenidoTxt;
            existing.UrlVideo = leccion.UrlVideo;
            existing.UrlAudio = leccion.UrlAudio;
            existing.Duracion = leccion.Duracion;
            existing.Orden = leccion.Orden;

            _context.Lecciones.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Eliminar lección
        public async Task<bool> DeleteAsync(int id)
        {
            var leccion = await _context.Lecciones.FindAsync(id);

            if (leccion == null)
                return false;

            _context.Lecciones.Remove(leccion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
