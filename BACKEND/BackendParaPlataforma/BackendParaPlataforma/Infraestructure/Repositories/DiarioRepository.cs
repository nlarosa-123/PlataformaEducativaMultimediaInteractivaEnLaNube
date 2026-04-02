using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class DiarioRepository : IDiarioRepository {

    private readonly AppDbContext _context;

    public DiarioRepository(AppDbContext context) {

        _context = context;
    }

    public async Task<DiarioEmocional> CrearDiario(DiarioEmocional diario) {

        _context.DiariosEmocionales.Add(diario);
        await _context.SaveChangesAsync();
        return diario;
    }

    public async Task<List<DiarioEmocional>> ObtenerDiariosUsuario(int idUsuario) {

        return await _context.DiariosEmocionales
            .Where(d => d.Id_Usuario == idUsuario)
            .ToListAsync();
    }

    public async Task<DiarioEmocional?> ObtenerDiarioPorId(int idDiario) {

        return await _context.DiariosEmocionales
            .FirstOrDefaultAsync(d => d.Id_Diario == idDiario);
    }

    public async Task<bool> ActualizarDiario(DiarioEmocional diario) {
        var existente = await _context.DiariosEmocionales.FindAsync(diario.Id_Diario);

        if (existente == null)
            return false;

        existente.Texto_Usuario = diario.Texto_Usuario;
        existente.Id_Emocion_Usuario = diario.Id_Emocion_Usuario;
        existente.Audio_Url = diario.Audio_Url;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarDiario(int id) {
        var diario = await _context.DiariosEmocionales.FindAsync(id);

        if (diario == null)
            return false;

        _context.DiariosEmocionales.Remove(diario);
        await _context.SaveChangesAsync();
        return true;
    }
}