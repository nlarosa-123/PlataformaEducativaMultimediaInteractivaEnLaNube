using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class ProgresoModuloUsuarioRepository : IProgresoModuloUsuarioRepository
    {
        private readonly AppDbContext _context;

        public ProgresoModuloUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos
        public async Task<IEnumerable<ProgresoModuloUsuario>> GetAllAsync()
        {
            return await _context.ProgresoModuloUsuarios
                .Include(p => p.Usuario)
                .Include(p => p.Modulos)
                .ToListAsync();
        }

        // 🔹 Obtener por ID
        public async Task<ProgresoModuloUsuario?> GetByIdAsync(int id)
        {
            return await _context.ProgresoModuloUsuarios
                .Include(p => p.Usuario)
                .Include(p => p.Modulos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // 🔹 Obtener progreso por usuario
        public async Task<IEnumerable<ProgresoModuloUsuario>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.ProgresoModuloUsuarios
                .Include(p => p.Modulos)
                .Where(p => p.IdUsuario == usuarioId)
                .ToListAsync();
        }

        // 🔹 Obtener progreso específico (usuario + módulo)
        public async Task<ProgresoModuloUsuario?> GetByUsuarioModuloAsync(int usuarioId, int moduloId)
        {
            return await _context.ProgresoModuloUsuarios
                .Include(p => p.Modulos)
                .FirstOrDefaultAsync(p => p.IdUsuario == usuarioId && p.IdModulo == moduloId);
        }

        // 🔹 Crear progreso
        public async Task<ProgresoModuloUsuario> CreateAsync(ProgresoModuloUsuario progreso)
        {
            await _context.ProgresoModuloUsuarios.AddAsync(progreso);
            await _context.SaveChangesAsync();
            return progreso;
        }

        // 🔹 Actualizar progreso
        public async Task<bool> UpdateAsync(ProgresoModuloUsuario progreso)
        {
            var existing = await _context.ProgresoModuloUsuarios.FindAsync(progreso.Id);

            if (existing == null)
                return false;

            existing.Porcentaje = progreso.Porcentaje;
            existing.Completado = progreso.Completado;
            existing.UltimaLeccion = progreso.UltimaLeccion;

            _context.ProgresoModuloUsuarios.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Eliminar progreso
        public async Task<bool> DeleteAsync(int id)
        {
            var progreso = await _context.ProgresoModuloUsuarios.FindAsync(id);

            if (progreso == null)
                return false;

            _context.ProgresoModuloUsuarios.Remove(progreso);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}