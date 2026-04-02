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
            return await _context.EstadisticaUsuario
                .ToListAsync();
        }

        public async Task AddAsync(EstadisticaUsuario stats)
        {
            await _context.EstadisticaUsuario.AddAsync(stats);
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<EstadisticaUsuario?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(EstadisticaUsuario stats)
        {
            throw new NotImplementedException();
        } 
    }



}