using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class AnalisisIARepository : IAnalisisIARepository {

    private readonly AppDbContext _context;

    public AnalisisIARepository(AppDbContext context) {

        _context = context;
    }

    public async Task<AnalisisIA> GuardarAnalisis(AnalisisIA analisis) {

        _context.AnalisisIA.Add(analisis);
        await _context.SaveChangesAsync();
        return analisis;
    }

    public async Task<AnalisisIA?> ObtenerAnalisisPorDiario(int idDiario) {
            
        return await _context.AnalisisIA
            .FirstOrDefaultAsync(a => a.Id_Diario == idDiario);
    }
}