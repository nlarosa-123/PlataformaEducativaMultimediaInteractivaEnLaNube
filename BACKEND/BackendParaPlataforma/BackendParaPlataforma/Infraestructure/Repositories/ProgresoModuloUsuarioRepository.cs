using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class ProgresoModuloUsuarioRepository : IProgresoModuloUsuarioRepository
    {
        private readonly AppDbContext _context;

        public ProgresoModuloUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgresoModuloUsuario>> ObtenerTodosAsync()
        {
            return await _context.ProgresoModuloUsuarios
                .Include(x => x.Usuario)
                .ToListAsync();
        }

        public async Task<ProgresoModuloUsuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.ProgresoModuloUsuarios
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        #region usuario
        public async Task<List<ProgresoModuloUsuario>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            return await _context.ProgresoModuloUsuarios
                .Where(x => x.IdUsuario == idUsuario)
                .Include(x => x.Usuario)
                .ToListAsync();
        }
        

        public async Task<ProgresoModuloUsuario?> ObtenerAsync(int idUsuario, int idModulo)
        {
            return await _context.ProgresoModuloUsuarios
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x =>
                    x.IdUsuario == idUsuario &&
                    x.IdModulo == idModulo);
        }
        #endregion

        public async Task<ProgresoModuloUsuario> CrearAsync(ProgresoModuloUsuario progreso)
        {
            await _context.ProgresoModuloUsuarios.AddAsync(progreso);
            return progreso;
        }

        public void Actualizar(ProgresoModuloUsuario progreso)
        {
            _context.ProgresoModuloUsuarios.Update(progreso);
        }

        public void Eliminar(ProgresoModuloUsuario progreso)
        {
            _context.ProgresoModuloUsuarios.Remove(progreso);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}