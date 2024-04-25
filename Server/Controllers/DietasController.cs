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
    public class DietasController : ControllerBase
    {
        private readonly ILogger<DietasController> _logger;
        private readonly GreenCoinHealthContext _context;

        public DietasController(ILogger<DietasController> logger, GreenCoinHealthContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dieta>>> GetDietas()
        {
            return await _context.Dietas.Include(d => d.DietaAlimentos).ThenInclude(da => da.Alimento).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dieta>> GetDieta(int id)
        {
            var dieta = await _context.Dietas
                .Include(d => d.DietaAlimentos)
                .ThenInclude(da => da.Alimento)
                .FirstOrDefaultAsync(d => d.DietaId == id);

            if (dieta == null)
            {
                return NotFound();
            }

            return dieta;
        }

        [HttpPost]
        public async Task<ActionResult<Dieta>> PostDieta(Dieta dieta)
        {
            _context.Dietas.Add(dieta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDieta), new { id = dieta.DietaId }, dieta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDieta(int id, Dieta dieta)
        {
            if (id != dieta.DietaId)
            {
                return BadRequest();
            }

            _context.Entry(dieta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDieta(int id)
        {
            var dieta = await _context.Dietas.FindAsync(id);
            if (dieta == null)
            {
                return NotFound();
            }

            _context.Dietas.Remove(dieta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
