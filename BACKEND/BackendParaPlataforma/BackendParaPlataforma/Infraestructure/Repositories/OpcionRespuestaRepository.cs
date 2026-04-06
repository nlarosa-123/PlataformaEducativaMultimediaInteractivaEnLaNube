using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendParaPlataforma.Infraestructure.Repositories
{
    public class OpcionRespuestaRepository : IOpcionRespuestaRepository
    {
        private readonly AppDbContext _context;

        public OpcionRespuestaRepository(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todas
        public async Task<IEnumerable<OpcionRespuesta>> GetAllAsync()
        {
            return await _context.OpcionRespuestas
                .Include(o => o.Pregunta)
                .ToListAsync();
        }

        // 🔹 Obtener por ID
        public async Task<OpcionRespuesta?> GetByIdAsync(int id)
        {
            return await _context.OpcionRespuestas
                .Include(o => o.Pregunta)
                .FirstOrDefaultAsync(o => o.IdOpcion == id);
        }

        // 🔹 Obtener opciones por pregunta
        public async Task<IEnumerable<OpcionRespuesta>> GetByPreguntaIdAsync(int preguntaId)
        {
            return await _context.OpcionRespuestas
                .Where(o => o.IdPregunta == preguntaId)
                .ToListAsync();
        }

        // 🔹 Obtener la opción correcta
        public async Task<OpcionRespuesta?> GetCorrectaByPreguntaIdAsync(int preguntaId)
        {
            return await _context.OpcionRespuestas
                .FirstOrDefaultAsync(o => o.IdPregunta == preguntaId && o.EsCorrecta);
        }

        // 🔹 Crear opción
        public async Task<OpcionRespuesta> CreateAsync(OpcionRespuesta opcion)
        {
            // 🔥 Validar que solo haya una correcta por pregunta
            if (opcion.EsCorrecta)
            {
                var existeCorrecta = await _context.OpcionRespuestas
                    .AnyAsync(o => o.IdPregunta == opcion.IdPregunta && o.EsCorrecta);

                if (existeCorrecta)
                    throw new Exception("Ya existe una opción correcta para esta pregunta");
            }

            await _context.OpcionRespuestas.AddAsync(opcion);
            await _context.SaveChangesAsync();
            return opcion;
        }

        // 🔹 Actualizar opción
        public async Task<bool> UpdateAsync(OpcionRespuesta opcion)
        {
            var existing = await _context.OpcionRespuestas
                .FirstOrDefaultAsync(o => o.IdOpcion == opcion.IdOpcion);

            if (existing == null)
                return false;

            // 🔥 Validar correcta única
            if (opcion.EsCorrecta && !existing.EsCorrecta)
            {
                var existeCorrecta = await _context.OpcionRespuestas
                    .AnyAsync(o => o.IdPregunta == opcion.IdPregunta && o.EsCorrecta);

                if (existeCorrecta)
                    throw new Exception("Ya existe una opción correcta para esta pregunta");
            }

            existing.TextoOpcion = opcion.TextoOpcion;
            existing.EsCorrecta = opcion.EsCorrecta;

            _context.OpcionRespuestas.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Eliminar opción
        public async Task<bool> DeleteAsync(int id)
        {
            var opcion = await _context.OpcionRespuestas
                .FirstOrDefaultAsync(o => o.IdOpcion == id);

            if (opcion == null)
                return false;

            _context.OpcionRespuestas.Remove(opcion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
