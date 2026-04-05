using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class ProgresoLeccionesUsuarioRepository : IProgresoLeccionesRepository
    {
        private readonly AppDbContext _context;

        public ProgresoLeccionesUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgresoLeccionesUsuario>> GetAllAsync()
        {
            return await _context.ProgresoLecciones
                .ToListAsync();
        }

        public async Task<ProgresoLeccionesUsuario?> GetByIdAsync(int id)
        {
            return await _context.ProgresoLecciones.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<ProgresoLeccionesUsuario>> GetByIdUserAsync(int id)
        {
            return await _context.ProgresoLecciones
                .Where(p => p.IdUsuario == id).ToListAsync();
        }


        public async Task AddAsync(ProgresoLeccionesUsuario progreso)
        {
            await _context.ProgresoLecciones.AddAsync(progreso);
        }

        public async Task DeleteAsync(ProgresoLeccionesUsuario progreso)
        {
            _context.ProgresoLecciones.Remove(progreso);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}