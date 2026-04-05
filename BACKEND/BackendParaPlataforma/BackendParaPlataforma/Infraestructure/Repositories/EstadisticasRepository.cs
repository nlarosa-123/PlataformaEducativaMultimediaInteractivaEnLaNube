using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class EstadisticasRepository : IEstadisticasRepository
    {
        private readonly AppDbContext _context;

        public EstadisticasRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EstadisticaUsuario>> GetAllAsync()
        {
            return await _context.Stats
                .ToListAsync();
        }

        public async Task<EstadisticaUsuario?> GetByIdAsync(int id)
        {
            return await _context.Stats
                .FirstOrDefaultAsync(s => s.IdUsuario == id);
        }


        public async Task AddAsync(EstadisticaUsuario stats)
        {
            await _context.Stats.AddAsync(stats);
        }

        public async Task DeleteAsync(EstadisticaUsuario stats)
        {
            _context.Stats.Remove(stats);
            await Task.CompletedTask;
        }



        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}