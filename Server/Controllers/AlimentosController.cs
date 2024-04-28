
using GreenCoinHealth.Server.Data;
using GreenCoinHealth.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GreenCoinHealth.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlimentosController : ControllerBase
    {
        private readonly ILogger<AlimentosController> _logger;
        private readonly GreenCoinHealthContext _context;

        public AlimentosController(ILogger<AlimentosController> logger, GreenCoinHealthContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet(Name = "GetAlimentos")]
        public async Task<ActionResult<IEnumerable<Alimentos>>> GetAlimentos()
        {
            return await _context.Alimentos.ToListAsync();
        }
        [HttpGet("{id}", Name = "GetProducto")]
        public async Task<ActionResult<Alimentos>> GetAlimentos(int id)
        {
            var Alimento = await _context.Alimentos.FindAsync(id);
            if (Alimento == null)
            {
                return NotFound();
            }
            return Alimento;

        }

        [HttpPost]
        public async Task<ActionResult<Alimentos>> Post(Alimentos alimentos)
        {
            _context.Add(alimentos);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult(nameof(GetAlimentos), new { id = alimentos.Id }, alimentos);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Alimentos alimentos)
        {
            if (id != alimentos.Id)
            {
                return BadRequest();
            }

            _context.Entry(alimentos).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Alimentos>> Delete(int id)
        {
            var alimento = await _context.Alimentos.FindAsync(id);

            if (alimento == null)
            { return NotFound(); }
            _context.Remove(alimento);
            await _context.SaveChangesAsync();

            return alimento;
        }






    }
}
