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

    public async Task<bool> ActualizarAnalisis(AnalisisIA analisis) {
        var existente = await _context.AnalisisIA.FindAsync(analisis.Id_Analisis);

        if (existente == null)
            return false;

        existente.Tono_Detectado = analisis.Tono_Detectado;
        existente.Confianza = analisis.Confianza;
        existente.Coincide_Usuario = analisis.Coincide_Usuario;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarAnalisis(int id) {
        var analisis = await _context.AnalisisIA.FindAsync(id);

        if (analisis == null)
            return false;

        _context.AnalisisIA.Remove(analisis);
        await _context.SaveChangesAsync();
        return true;
    }
}