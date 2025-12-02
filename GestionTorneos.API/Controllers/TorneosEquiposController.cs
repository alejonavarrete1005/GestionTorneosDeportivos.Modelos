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
    public class TorneoEquipoController : ControllerBase
    {
        private readonly GestionTorneosAPIContext _context;

        public TorneoEquipoController(GestionTorneosAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TorneoEquipo>>> GetTorneoEquipos()
        {
            return await _context
                .TorneosEquipos
                .Include(te => te.Torneo)
                .Include(te => te.Equipo)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TorneoEquipo>> GetTorneoEquipo(int id)
        {
            var te = await _context
                .TorneosEquipos
                .Include(x => x.Torneo)
                .Include(x => x.Equipo)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (te == null)
                return NotFound();

            return te;
        }

        [HttpPost]
        public async Task<ActionResult<TorneoEquipo>> PostTorneoEquipo(TorneoEquipo te)
        {
            _context.TorneosEquipos.Add(te);
            await _context.SaveChangesAsync();

            te.Torneo = await _context.Torneos.FindAsync(te.TorneoId);
            te.Equipo = await _context.Equipos.FindAsync(te.EquipoId);

            return CreatedAtAction("GetTorneoEquipo", new { id = te.Id }, te);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTorneoEquipo(int id, TorneoEquipo te)
        {
            if (id != te.Id)
                return BadRequest();

            _context.Entry(te).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TorneoEquipoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTorneoEquipo(int id)
        {
            var te = await _context.TorneosEquipos.FindAsync(id);

            if (te == null)
                return NotFound();

            _context.TorneosEquipos.Remove(te);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TorneoEquipoExists(int id)
        {
            return _context.TorneosEquipos.Any(x => x.Id == id);
        }
    }

}
