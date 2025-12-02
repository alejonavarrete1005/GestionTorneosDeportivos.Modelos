using GestionTorneosDeportivos.Modelos;
using GestionTorneosDeportivos.Modelos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ResultadosController : ControllerBase
{
    private readonly GestionTorneosAPIContext _context;

    public ResultadosController(GestionTorneosAPIContext context)
    {
        _context = context;
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> RegistrarResultado(ResultadoPartidoDTO dto)
    {
        var partido = await _context.Partidos
            .Include(p => p.EquipoLocal)
            .Include(p => p.EquipoVisitante)
            .Where(p => p.Id == dto.PartidoId)
            .FirstOrDefaultAsync();

        if (partido == null)
            return NotFound("Partido no encontrado.");

        if (partido.Jugado)
            return BadRequest("Este partido ya tiene un resultado registrado.");

    
        //  GUARDAR GOLES
       
        foreach (var gol in dto.Goles)
        {
            var jugador = await _context.Jugadores.FindAsync(gol.JugadorId);
            if (jugador == null)
                return NotFound($"Jugador {gol.JugadorId} no existe.");

            var evento = new EventoPartido
            {
                PartidoId = dto.PartidoId,
                JugadorId = gol.JugadorId,
                Minuto = gol.Minuto,
                Tipo = "Gol"
            };

            _context.EventosPartidos.Add(evento);
        }

        // ======================
        //  GUARDAR TARJETAS
        // ======================
        foreach (var card in dto.Tarjetas)
        {
            var jugador = await _context.Jugadores.FindAsync(card.JugadorId);
            if (jugador == null)
                return NotFound($"Jugador {card.JugadorId} no existe.");

            var evento = new EventoPartido
            {
                PartidoId = dto.PartidoId,
                JugadorId = card.JugadorId,
                Minuto = card.Minuto,
                Tipo = card.Tipo
            };

            _context.EventosPartidos.Add(evento);
        }

        // ======================
        //  MARCAR PARTIDO COMO JUGADO
        // ======================
        partido.Jugado = true;
        partido.GolesLocal = dto.GolesLocal;
        partido.GolesVisitante = dto.GolesVisitante;

        // ======================
        //  ACTUALIZAR ESTADISTICAS
        // ======================
        await ActualizarEstadisticas(partido);

        await _context.SaveChangesAsync();

        return Ok("Resultado registrado correctamente.");
    }

    // ===========================================
    //  MÉTODO PARA ACTUALIZAR ESTADISTICAS
    // ===========================================
    private async Task ActualizarEstadisticas(Partido partido)
    {
        // Ajusta estos nombres si tu entidad Partido usa otros nombres de FK
        // (EquipoLocalId / EquipoVisitanteId ó LocalId / VisitanteId)
        int localId = partido.EquipoLocalId;       // <-- si tu propiedad se llama LocalId cámbialo aquí
        int visitanteId = partido.EquipoVisitanteId; // <-- o VisitanteId según tu modelo
        int torneoId = partido.TorneoId;

        // Obtener los registros TorneoEquipo (estadísticas por torneo)
        var teLocal = await _context.TorneosEquipos
            .FirstOrDefaultAsync(te => te.TorneoId == torneoId && te.EquipoId == localId);

        var teVis = await _context.TorneosEquipos
            .FirstOrDefaultAsync(te => te.TorneoId == torneoId && te.EquipoId == visitanteId);

        if (teLocal == null || teVis == null)
        {
            // Si no existen, mejor devolver una excepción o manejar el error
            throw new InvalidOperationException("No se encontró TorneoEquipo para local o visitante. Asegúrate de que ambos equipos estén inscritos en el torneo.");
        }

        // Actualizar goles y diferencia
        teLocal.GolesFavor += partido.GolesLocal;
        teLocal.GolesContra += partido.GolesVisitante;
        teLocal.Diferencia = teLocal.GolesFavor - teLocal.GolesContra;

        teVis.GolesFavor += partido.GolesVisitante;
        teVis.GolesContra += partido.GolesLocal;
        teVis.Diferencia = teVis.GolesFavor - teVis.GolesContra;

        // Asignar puntos (3, 1, 0)
        if (partido.GolesLocal > partido.GolesVisitante)
        {
            teLocal.Puntos += 3;
            partido.GanadorId = teLocal.EquipoId;
        }
        else if (partido.GolesLocal < partido.GolesVisitante)
        {
            teVis.Puntos += 3;
            partido.GanadorId = teVis.EquipoId;
        }
        else
        {
            teLocal.Puntos += 1;
            teVis.Puntos += 1;
            partido.GanadorId = null; // empate: sin ganador (o manejar penales si aplica)
        }

        // Marcar cambios en EF Core (opcional, SaveChanges se hace fuera)
        _context.Entry(teLocal).State = EntityState.Modified;
        _context.Entry(teVis).State = EntityState.Modified;
        _context.Entry(partido).State = EntityState.Modified;

        // NOTA: No llamo a SaveChanges aquí si ya lo haces en el método que invocó a ActualizarEstadisticas.
        // Si prefieres que este método guarde, agrega: await _context.SaveChangesAsync();
    }

}
