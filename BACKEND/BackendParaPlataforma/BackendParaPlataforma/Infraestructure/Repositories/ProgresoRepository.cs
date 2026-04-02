using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class ProgresoRepository : IProgresoRepository
{
    private readonly AppDbContext _context;

    public ProgresoRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<ProgresoUsuario> GuardarProgreso(ProgresoUsuario progreso) {
        _context.ProgresoUsuarios.Add(progreso);
        await _context.SaveChangesAsync();
        return progreso;
    }

    public async Task<ProgresoUsuario?> ObtenerProgresoUsuario(int idUsuario, string modulo) {
        return await _context.ProgresoUsuarios
            .FirstOrDefaultAsync(p => p.Id_Usuario == idUsuario && p.Modulo == modulo);
    }

    public async Task<bool> ActualizarProgreso(ProgresoUsuario progreso) {
        var existente = await _context.ProgresoUsuarios.FindAsync(progreso.Id_Progreso);

        if (existente == null)
            return false;

        existente.Modulo = progreso.Modulo;
        existente.Porcentaje = progreso.Porcentaje;
        existente.Ultima_Actualizacion = DateTime.Now;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarProgreso(int id) {
        var progreso = await _context.ProgresoUsuarios.FindAsync(id);

        if (progreso == null)
            return false;

        _context.ProgresoUsuarios.Remove(progreso);
        await _context.SaveChangesAsync();
        return true;
    }
}