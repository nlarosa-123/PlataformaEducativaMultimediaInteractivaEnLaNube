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

        public async Task<List<Emociones>> GetAllAsync()
        {
            return await _context.Emociones
                .ToListAsync();
        }

        public async Task<Emociones?> GetByIdAsync(int id)
        {
            return await _context.Emociones
                .FirstOrDefaultAsync(e => e.IdEmocion == id);
        }

        public async Task AddAsync(Emociones emocion)
        {
            await _context.Emociones.AddAsync(emocion);
        }

        public async Task DeleteAsync(Emociones emocion)
        {
            _context.Emociones.Remove(emocion);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
