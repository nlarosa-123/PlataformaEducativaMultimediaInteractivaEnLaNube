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

        public async Task<List<Lecciones>> GetAllAsync()
        {
            return await _context.Lecciones
                .ToListAsync();
        }

        public async Task<Lecciones?> GetByIdAsync(int id)
        {
            return await _context.Lecciones
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(Lecciones l)
        {
            await _context.Lecciones.AddAsync(l);
        }

        public async Task DeleteAsync(Lecciones l)
        {
            _context.Lecciones.Remove(l);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
