using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class DiarioRepository : IDiarioRepository
    {
        private readonly AppDbContext _context;

        public DiarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DiarioEmocional diarioEmocional)
        {
            await _context.DiariosEmocionales.AddAsync(diarioEmocional);
        }

        public async Task<List<DiarioEmocional>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.DiariosEmocionales
                .Where(d => d.UsuarioId == usuarioId)
                .Include(d => d.Emocion)
                .OrderByDescending(d => d.Fecha)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
