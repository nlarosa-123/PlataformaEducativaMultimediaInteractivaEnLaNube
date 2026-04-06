using BackendParaPlataforma.dtos;
using BackendParaPlataforma.Entities;
using BackendParaPlataforma.Infraestructure.Persistence;
using BackendParaPlataforma.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace BackendParaPlataforma.FuncionesAux
{
    public class MetodosAux
    {
        private readonly AppDbContext _context;

        public MetodosAux(AppDbContext context)
        {
            _context = context;
        }

        // MÉTODO PRINCIPAL
        public async Task CrearActualizarEstUsuario(int idUsuario)
        {
            var diarios = await ObtenerDiariosUsuario(idUsuario);
            var analisis = await ObtenerAnalisisDeDiarios(idUsuario);

            double porcentajeIA = CalcularPorcentajeIA(analisis);
            string emocionFrecuente = ObtenerEmocionMasFrecuente(diarios);
            int racha = CalcularRacha(diarios);

            var estadistica = await _context.EstadisticaUsuario
                .FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);

            if (estadistica == null)
            {
                estadistica = new EstadisticaUsuario
                {
                    IdUsuario = idUsuario,
                    PorcentajeCoincidenciaIA = (decimal)porcentajeIA,
                    EmocionMasFrecuente = emocionFrecuente,
                    RachaDiasRegistrados = racha,
                    UltimaActualizacion = DateTime.Now
                };

                _context.EstadisticaUsuario.Add(estadistica);
            }
            else
            {
                estadistica.PorcentajeCoincidenciaIA = (decimal)porcentajeIA;
                estadistica.EmocionMasFrecuente = emocionFrecuente;
                estadistica.RachaDiasRegistrados = racha;
                estadistica.UltimaActualizacion = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }

        // Obtener diarios del usuario
        public async Task<List<DiarioEmocional>> ObtenerDiariosUsuario(int idUsuario)
        {
            return await _context.DiariosEmocionales
                .Where(d => d.Id_Usuario == idUsuario)
                .ToListAsync();
        }

        // Obtener análisis IA
        public async Task<List<AnalisisIA>> ObtenerAnalisisDeDiarios(int idUsuario)
        {
            return await _context.AnalisisIA
                .Where(a => a.DiarioEmocional != null && a.DiarioEmocional.Id_Usuario == idUsuario)
                .ToListAsync();
        }

        // Porcentaje IA
        public double CalcularPorcentajeIA(List<AnalisisIA> analisis)
        {
            if (analisis.Count == 0) return 0;

            int coincidencias = analisis.Count(a => a.Coincide_Usuario == true);

            return (double)coincidencias / analisis.Count * 100;
        }

        // Emoción más frecuente
        public string ObtenerEmocionMasFrecuente(List<DiarioEmocional> diarios)
        {
            if (!diarios.Any()) return "";

            return diarios
                .GroupBy(d => d.Id_Emocion_Usuario)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key.ToString())
                .FirstOrDefault() ?? "";
        }

        // Racha
        public int CalcularRacha(List<DiarioEmocional> diarios)
        {
            var fechas = diarios
                .Select(d => d.Fecha.Date)
                .Distinct()
                .OrderByDescending(f => f)
                .ToList();

            if (!fechas.Any()) return 0;

            int racha = 1;
            DateTime fechaBase = fechas[0];

            for (int i = 1; i < fechas.Count; i++)
            {
                if (fechas[i] == fechaBase.AddDays(-racha))
                    racha++;
                else
                    break;
            }

            /*DateTime hoy = DateTime.Today;

            foreach (var fecha in fechas)
            {
                if (fecha == hoy.AddDays(-racha))
                    racha++;
                else
                    break;
            }*/

            return racha;
        }

        public async Task CalcularPorcentajeModulo(int modId, int userId)
        {
            //Obtener todas las lecciones de un modulo
            var lecciones = await ObtenerLeccionesModulo(modId);

            List<int> idLecciones = lecciones.Select(l => l.Id).ToList(); 

            //Obtener el progreso de las lecciones de un usuario en ese modulo 
            var progresoLecciones = await ObtenerProgresoLecciones(idLecciones, userId);

            //Calcular el porcentaje de lecciones completadas
            double porcentaje = CalcularPorcentajeCompletados(progresoLecciones);

            //Actualizar la tabla con el nuevo valor de Porcentaje
            var progresoModulo  = await _context.ProgresoModuloUsuarios.
                FirstOrDefaultAsync(p => p.IdUsuario == userId && p.IdModulo == modId);

            progresoModulo.Porcentaje = (decimal) porcentaje;

            if (porcentaje ==  100.0)
            {
                progresoModulo.Completado = true; 
            }

            await _context.SaveChangesAsync();
        }

        public async Task <List<Lecciones>> ObtenerLeccionesModulo(int modId)
        {
            return await _context.Lecciones
                .Where(l => l.IdModulo == modId)
                .ToListAsync();
        }

        public async Task <List<ProgresoLeccionUsuario>> ObtenerProgresoLecciones(List<int> idLecciones, int userId)
        {
            return await _context.ProgresoLeccionUsuario
                .Where(plu => idLecciones.Contains(plu.Id_Leccion) && plu.Id_Usuario == userId)
                .ToListAsync(); 
        }

        public double CalcularPorcentajeCompletados (List<ProgresoLeccionUsuario> pLecciones)
        {
            var completados = pLecciones.Count(p => p.Completado == true);

            return (double)completados / pLecciones.Count * 100; 
        }
    }
}
