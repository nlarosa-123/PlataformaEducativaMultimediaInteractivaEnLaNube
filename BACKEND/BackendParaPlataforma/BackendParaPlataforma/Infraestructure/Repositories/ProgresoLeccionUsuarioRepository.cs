using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class ProgresoLeccionUsuarioRepository : IProgresoLeccionUsuarioRepository
    {
        private readonly AppDbContext _context;

        public ProgresoLeccionUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos
        public async Task<IEnumerable<ProgresoLeccionUsuario>> GetAllAsync()
        {
            return await _context.ProgresoLeccionUsuario
                .Include(p => p.Usuario)
                .Include(p => p.Leccion)
                .ToListAsync();
        }

        // 🔹 Obtener por ID
        public async Task<ProgresoLeccionUsuario?> GetByIdAsync(int id)
        {
            return await _context.ProgresoLeccionUsuario
                .Include(p => p.Usuario)
                .Include(p => p.Leccion)
                .FirstOrDefaultAsync(p => p.Id_Progreso == id);
        }

        // 🔹 Obtener progreso por usuario
        public async Task<IEnumerable<ProgresoLeccionUsuario>> GetByUsuarioAsync(int usuarioId)
        {
            return await _context.ProgresoLeccionUsuario
                .Include(p => p.Leccion)
                .Where(p => p.Id_Usuario == usuarioId)
                .ToListAsync();
        }

        // 🔹 Obtener progreso por lección
        public async Task<IEnumerable<ProgresoLeccionUsuario>> GetByLeccionAsync(int leccionId)
        {
            return await _context.ProgresoLeccionUsuario
                .Include(p => p.Usuario)
                .Where(p => p.Id_Leccion == leccionId)
                .ToListAsync();
        }

        // 🔹 Obtener progreso específico (usuario + lección)
        public async Task<ProgresoLeccionUsuario?> GetByUsuarioLeccionAsync(int usuarioId, int leccionId)
        {
            return await _context.ProgresoLeccionUsuario
                .FirstOrDefaultAsync(p => p.Id_Usuario == usuarioId && p.Id_Leccion == leccionId);
        }

        // 🔹 Crear progreso
        public async Task<ProgresoLeccionUsuario> CreateAsync(ProgresoLeccionUsuario progreso)
        {
            // 🔥 Evitar duplicados (clave compuesta lógica)
            var existe = await _context.ProgresoLeccionUsuario
                .AnyAsync(p => p.Id_Usuario == progreso.Id_Usuario && p.Id_Leccion == progreso.Id_Leccion);

            if (existe)
                throw new Exception("Ya existe progreso para este usuario y lección");

            await _context.ProgresoLeccionUsuario.AddAsync(progreso);
            await _context.SaveChangesAsync();
            return progreso;
        }

        // 🔹 Actualizar progreso
        public async Task<bool> UpdateAsync(ProgresoLeccionUsuario progreso)
        {
            var existing = await _context.ProgresoLeccionUsuario
                .FirstOrDefaultAsync(p => p.Id_Progreso == progreso.Id_Progreso);

            if (existing == null)
                return false;

            existing.Completado = progreso.Completado;
            existing.Tiempo_Visualizado = progreso.Tiempo_Visualizado;

            // 🔥 Si se completa, guardar fecha
            if (progreso.Completado && existing.Fecha_Completado == null)
            {
                existing.Fecha_Completado = DateTime.UtcNow;
            }

            _context.ProgresoLeccionUsuario.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Eliminar progreso
        public async Task<bool> DeleteAsync(int id)
        {
            var progreso = await _context.ProgresoLeccionUsuario
                .FirstOrDefaultAsync(p => p.Id_Progreso == id);

            if (progreso == null)
                return false;

            _context.ProgresoLeccionUsuario.Remove(progreso);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
