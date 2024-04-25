using GreenCoinHealth.Server.Data;
using GreenCoinHealth.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GreenCoinHealth.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DietaAlimentosController : ControllerBase
    {
        private readonly ILogger<DietaAlimentosController> _logger;
        private readonly GreenCoinHealthContext _context;

        public DietaAlimentosController(ILogger<DietaAlimentosController> logger, GreenCoinHealthContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddDietaAlimento(DietaAlimento dietaAlimento)
        {
            _context.DietaAlimentos.Add(dietaAlimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDieta", "Dietas", new { id = dietaAlimento.DietaId }, dietaAlimento);
        }

        [HttpDelete("{dietaId}/{alimentoId}")]
        public async Task<ActionResult> DeleteDietaAlimento(int dietaId, int alimentoId)
        {
            var dietaAlimento = await _context.DietaAlimentos
                .FirstOrDefaultAsync(da => da.DietaId == dietaId && da.AlimentoId == alimentoId);

            if (dietaAlimento == null)
            {
                return NotFound();
            }

            _context.DietaAlimentos.Remove(dietaAlimento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
