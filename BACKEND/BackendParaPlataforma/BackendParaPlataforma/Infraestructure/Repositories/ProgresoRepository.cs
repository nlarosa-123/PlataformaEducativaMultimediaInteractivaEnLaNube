using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class ProgresoRepository : IProgresoRepository
{
    private readonly AppDbContext _context;

    public ProgresoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProgresoUsuario> GuardarProgreso(ProgresoUsuario progreso)
    {
        _context.ProgresoUsuarios.Add(progreso);
        await _context.SaveChangesAsync();
        return progreso;
    }

    public async Task<ProgresoUsuario?> ObtenerProgresoUsuario(int idUsuario, string modulo)
    {
        return await _context.ProgresoUsuarios
            .FirstOrDefaultAsync(p => p.Id_Usuario == idUsuario && p.Modulo == modulo);
    }
}