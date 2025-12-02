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
    public class TorneosEquiposController : ControllerBase
    {
        private readonly GestionTorneosAPIContext _context;

        public TorneosEquiposController(GestionTorneosAPIContext context)
        {
            _context = context;
        }

        // GET: api/TorneosEquipos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TorneoEquipo>>> GetTorneoEquipo()
        {
            return await _context.TorneosEquipos.ToListAsync();
        }

        // GET: api/TorneosEquipos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TorneoEquipo>> GetTorneoEquipo(int id)
        {
            var torneoEquipo = await _context.TorneosEquipos.FindAsync(id);

            if (torneoEquipo == null)
            {
                return NotFound();
            }

            return torneoEquipo;
        }

        // PUT: api/TorneosEquipos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTorneoEquipo(int id, TorneoEquipo torneoEquipo)
        {
            if (id != torneoEquipo.Id)
            {
                return BadRequest();
            }

            _context.Entry(torneoEquipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TorneoEquipoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TorneosEquipos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TorneoEquipo>> PostTorneoEquipo(TorneoEquipo torneoEquipo)
        {
            _context.TorneosEquipos.Add(torneoEquipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTorneoEquipo", new { id = torneoEquipo.Id }, torneoEquipo);
        }

        // DELETE: api/TorneosEquipos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTorneoEquipo(int id)
        {
            var torneoEquipo = await _context.TorneosEquipos.FindAsync(id);
            if (torneoEquipo == null)
            {
                return NotFound();
            }

            _context.TorneosEquipos.Remove(torneoEquipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TorneoEquipoExists(int id)
        {
            return _context.TorneosEquipos.Any(e => e.Id == id);
        }
    }
}
