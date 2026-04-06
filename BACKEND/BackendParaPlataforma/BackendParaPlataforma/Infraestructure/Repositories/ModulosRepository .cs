using BackendParaPlataforma.dtos;
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

        // 🔹 Obtener todos los módulos
        public async Task<IEnumerable<ModuloDto>> GetAllAsync()
        {
            return await _context.Modulos
                .Select(m => new ModuloDto
                {
                    IdModulo = m.IdModulo,
                    Titulo = m.Titulo,
                    Descripcion = m.Descripcion,
                    Lecciones = m.Lecciones.Select(l => new LeccionDto
                    {
                        IdLeccion = l.Id
                    }).ToList()
                })
                .ToListAsync();
        }

        // 🔹 Obtener por ID
        public async Task<Modulos?> GetByIdAsync(int id)
        {
            return await _context.Modulos
                .Include(m => m.Lecciones)
                .Include(m => m.ProgresoModuloUsuarios)
                .FirstOrDefaultAsync(m => m.IdModulo == id);
        }

        // 🔹 Crear módulo
        public async Task<Modulos> CreateAsync(Modulos modulo)
        {
            await _context.Modulos.AddAsync(modulo);
            await _context.SaveChangesAsync();
            return modulo;
        }

        // 🔹 Actualizar módulo
        public async Task<bool> UpdateAsync(Modulos modulo)
        {
            var existing = await _context.Modulos.FindAsync(modulo.IdModulo);
            if (existing == null)
                return false;

            existing.Titulo = modulo.Titulo;
            existing.Descripcion = modulo.Descripcion;

            _context.Modulos.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Eliminar módulo
        public async Task<bool> DeleteAsync(int id)
        {
            var modulo = await _context.Modulos.FindAsync(id);
            if (modulo == null)
                return false;

            _context.Modulos.Remove(modulo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
