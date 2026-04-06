using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories

{
    public class ProgresoUsuarioRepository : IProgresoUsuarioRepository
    {
        private readonly AppDbContext _context;

        public ProgresoUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        // ?? Obtener todos
        public async Task<IEnumerable<ProgresoUsuario>> GetAllAsync()
        {
            return await _context.ProgresoUsuarios
                .Include(p => p.Usuario)
                .ToListAsync();
        }

        // ?? Obtener por ID
        public async Task<ProgresoUsuario?> GetByIdAsync(int id)
        {
            return await _context.ProgresoUsuarios
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id_Progreso == id);
        }

        // ?? Obtener por usuario
        public async Task<IEnumerable<ProgresoUsuario>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.ProgresoUsuarios
                .Where(p => p.Id_Usuario == usuarioId)
                .ToListAsync();
        }

        // ?? Crear progreso
        public async Task<ProgresoUsuario> CreateAsync(ProgresoUsuario progreso)
        {
            progreso.Ultima_Actualizacion = DateTime.UtcNow;

            await _context.ProgresoUsuarios.AddAsync(progreso);
            await _context.SaveChangesAsync();
            return progreso;
        }

        // ?? Actualizar progreso
        public async Task<bool> UpdateAsync(ProgresoUsuario progreso)
        {
            var existing = await _context.ProgresoUsuarios
                .FirstOrDefaultAsync(p => p.Id_Progreso == progreso.Id_Progreso);

            if (existing == null)
                return false;

            existing.Modulo = progreso.Modulo;
            existing.Porcentaje = progreso.Porcentaje;
            existing.Ultima_Actualizacion = DateTime.UtcNow;

            _context.ProgresoUsuarios.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // ?? Eliminar progreso
        public async Task<bool> DeleteAsync(int id)
        {
            var progreso = await _context.ProgresoUsuarios
                .FirstOrDefaultAsync(p => p.Id_Progreso == id);

            if (progreso == null)
                return false;

            _context.ProgresoUsuarios.Remove(progreso);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

