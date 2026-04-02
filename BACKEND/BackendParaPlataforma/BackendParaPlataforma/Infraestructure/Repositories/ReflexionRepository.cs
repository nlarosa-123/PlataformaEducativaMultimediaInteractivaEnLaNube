using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class ReflexionRepository : IReflexionRepository {

    private readonly AppDbContext _context;

    public ReflexionRepository(AppDbContext context) {

        _context = context;
    }

    public async Task<ReflexionMejora> CrearReflexion(ReflexionMejora reflexion) {

        _context.ReflexionesMejora.Add(reflexion);
        await _context.SaveChangesAsync();
        return reflexion;
    }

    public async Task<List<ReflexionMejora>> ObtenerReflexionesPorDiario(int idDiario) {

        return await _context.ReflexionesMejora
            .Where(r => r.Id_Diario == idDiario)
            .ToListAsync();
    }

    public async Task<bool> ActualizarReflexion(ReflexionMejora reflexion) {
        var existente = await _context.ReflexionesMejora.FindAsync(reflexion.Id_Reflexion);

        if (existente == null)
            return false;

        existente.Texto_Reflexion = reflexion.Texto_Reflexion;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarReflexion(int id) {
        var reflexion = await _context.ReflexionesMejora.FindAsync(id);

        if (reflexion == null)
            return false;

        _context.ReflexionesMejora.Remove(reflexion);
        await _context.SaveChangesAsync();
        return true;
    }
}