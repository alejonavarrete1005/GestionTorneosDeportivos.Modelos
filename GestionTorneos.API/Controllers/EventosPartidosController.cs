using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionTorneosDeportivos.Modelos;

namespace GestionTorneos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosPartidoController : ControllerBase
    {
        private readonly GestionTorneosAPIContext _context;

        public EventosPartidoController(GestionTorneosAPIContext context)
        {
            _context = context;
        }

        // GET: api/EventosPartido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoPartido>>> GetEventos()
        {
            return await _context
                .EventosPartidos
                .Include(e => e.Partido)
                .Include(e => e.Jugador)
                .ThenInclude(j => j.Equipo)
                .ToListAsync();
        }

        // GET: api/EventosPartido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoPartido>> GetEvento(int id)
        {
            var evento = await _context
                .EventosPartidos
                .Include(e => e.Partido)
                .Include(e => e.Jugador)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (evento == null)
                return NotFound();

            return evento;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<EventoPartido>> PostEvento(EventoPartido evento)
        {
            _context.EventosPartidos.Add(evento);
            await _context.SaveChangesAsync();

            evento.Partido = await _context.Partidos.FindAsync(evento.PartidoId);
            evento.Jugador = await _context.Jugadores.FindAsync(evento.JugadorId);

            return CreatedAtAction("GetEvento", new { id = evento.Id }, evento);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, EventoPartido evento)
        {
            if (id != evento.Id)
                return BadRequest();

            _context.Entry(evento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.EventosPartidos.FindAsync(id);
            if (evento == null)
                return NotFound();

            _context.EventosPartidos.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoExists(int id)
        {
            return _context.EventosPartidos.Any(e => e.Id == id);
        }
    }

}
