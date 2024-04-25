using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenCoinHealth.Server.Models;
using GreenCoinHealth.Server.Data;

namespace GreenCoinHealth.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DificultadesController : ControllerBase
    {
        private readonly ILogger<DificultadesController> _logger;
        private readonly GreenCoinHealthContext _context;

        public DificultadesController(ILogger<DificultadesController> logger, GreenCoinHealthContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dificultad>>> GetDificultades()
        {
            return await _context.Dificultades.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dificultad>> GetDificultad(int id)
        {
            var dificultad = await _context.Dificultades.FindAsync(id);

            if (dificultad == null)
            {
                return NotFound();
            }

            return dificultad;
        }

        [HttpPost]
        public async Task<ActionResult<Dificultad>> PostDificultad(Dificultad dificultad)
        {
            _context.Dificultades.Add(dificultad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDificultad), new { id = dificultad.DificultadId }, dificultad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDificultad(int id, Dificultad dificultad)
        {
            if (id != dificultad.DificultadId)
            {
                return BadRequest();
            }

            _context.Entry(dificultad).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Dificultades.Any(e => e.DificultadId == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDificultad(int id)
        {
            var dificultad = await _context.Dificultades.FindAsync(id);
            if (dificultad == null)
            {
                return NotFound();
            }

            _context.Dificultades.Remove(dificultad);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}