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
}