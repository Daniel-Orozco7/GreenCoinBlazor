using GreenCoinHealth.Server.Data;
using GreenCoinHealth.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenCoinHealth.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EjerciciosController : ControllerBase
    {
        private readonly ILogger<EjerciciosController> _logger;
        private readonly GreenCoinHealthContext _context;

        public EjerciciosController(ILogger<EjerciciosController> logger,   GreenCoinHealthContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ejercicio>>> GetEjercicios()
        {
            return await _context.Ejercicios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ejercicio>> GetEjercicio(int id)
        {
            var ejercicio = await _context.Ejercicios.FindAsync(id);

            if (ejercicio == null)
            {
                return NotFound();
            }

            return ejercicio;
        }

        [HttpPost]
        public async Task<ActionResult<Ejercicio>> PostEjercicio(Ejercicio ejercicio)
        {
            _context.Ejercicios.Add(ejercicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEjercicio), new { id = ejercicio.EjercicioId }, ejercicio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEjercicio(int id, Ejercicio ejercicio)
        {
            if (id != ejercicio.EjercicioId)
            {
                return BadRequest();
            }

            _context.Entry(ejercicio).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Ejercicios.Any(e => e.EjercicioId == id))
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
        public async Task<IActionResult> DeleteEjercicio(int id)
        {
            var ejercicio = await _context.Ejercicios.FindAsync(id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            _context.Ejercicios.Remove(ejercicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
