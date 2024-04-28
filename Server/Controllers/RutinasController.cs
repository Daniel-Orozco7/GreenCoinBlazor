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
    public class RutinasController : ControllerBase
    {
        private readonly ILogger<RutinasController> _logger;
        private readonly GreenCoinHealthContext _context;

        public RutinasController(ILogger<RutinasController> logger, GreenCoinHealthContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rutina>>> GetRutinas()
        {
            return await _context.Rutinas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rutina>> GetRutina(int id)
        {
            var rutina = await _context.Rutinas.FirstOrDefaultAsync(r => r.RutinaId == id);

            if (rutina == null)
            {
                return NotFound();
            }

            return rutina;
        }

        [HttpPost]
        public async Task<ActionResult<Rutina>> PostRutina(Rutina rutina)
        {
            _context.Rutinas.Add(rutina);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRutina), new { id = rutina.RutinaId }, rutina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRutina(int id, Rutina rutina)
        {
            if (id != rutina.RutinaId)
            {
                return BadRequest();
            }

            _context.Entry(rutina).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RutinaExists(id))
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
        public async Task<IActionResult> DeleteRutina(int id)
        {
            var rutina = await _context.Rutinas.FindAsync(id);
            if (rutina == null)
            {
                return NotFound();
            }

            _context.Rutinas.Remove(rutina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RutinaExists(int id)
        {
            return _context.Rutinas.Any(e => e.RutinaId == id);
        }
    }
}













//using GreenCoinHealth.Server.Data;
//using GreenCoinHealth.Server.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace GreenCoinHealth.Server.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class RutinasController : ControllerBase
//    {
//        private readonly ILogger<RutinasController> _logger;
//        private readonly GreenCoinHealthContext _context;

//        public RutinasController(ILogger<RutinasController> logger, GreenCoinHealthContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Rutina>>> GetRutinas()
//        {
//            return await _context.Rutinas.Include(r => r.Ejercicioslist).ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Rutina>> GetRutina(int id)
//        {
//            var rutina = await _context.Rutinas.Include(r => r.Ejercicioslist).FirstOrDefaultAsync(r => r.RutinaId == id);

//            if (rutina == null)
//            {
//                return NotFound();
//            }

//            return rutina;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Rutina>> PostRutina(Rutina rutina)
//        {
//            _context.Rutinas.Add(rutina);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetRutina), new { id = rutina.RutinaId }, rutina);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutRutina(int id, Rutina rutina)
//        {
//            if (id != rutina.RutinaId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(rutina).State = EntityState.Modified;
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!_context.Rutinas.Any(e => e.RutinaId == id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteRutina(int id)
//        {
//            var rutina = await _context.Rutinas.FindAsync(id);
//            if (rutina == null)
//            {
//                return NotFound();
//            }

//            _context.Rutinas.Remove(rutina);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}
