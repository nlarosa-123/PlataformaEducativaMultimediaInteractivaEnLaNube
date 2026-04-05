using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class ModulosRepository : IModulosRepository
    {
        private readonly AppDbContext _context;

        public ModulosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Modulos>> GetAllAsync()
        {
            return await _context.Modulos
                .ToListAsync();
        }

        public async Task<Modulos?> GetByIdAsync(int id)
        {
            return await _context.Modulos
                .FirstOrDefaultAsync(m => m.IdModulo == id);
        }
            
        public async Task<Modulos?> GetByTituloAsync (string titulo)
        {
            return await _context.Modulos
                .FirstOrDefaultAsync(m => m.Titulo == titulo);
        }

        public async Task AddAsync(Modulos mods)
        {
            await _context.Modulos.AddAsync(mods);
        }

        public async Task DeleteAsync(Modulos mods)
        {
            _context.Modulos.Remove(mods);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}