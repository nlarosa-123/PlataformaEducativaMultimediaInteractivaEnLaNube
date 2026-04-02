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
}