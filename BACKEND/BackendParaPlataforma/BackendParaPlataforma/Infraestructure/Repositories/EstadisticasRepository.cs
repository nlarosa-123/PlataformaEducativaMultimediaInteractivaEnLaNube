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

        public async Task<List<Estadisticas>> GetAllAsync()
        {
            return await _context.Estadisticas
                .ToListAsync();
        }

        public async Task AddAsync(Estadisticas stats)
        {
            await _context.Estadisticas.AddAsync(stats);
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }



}