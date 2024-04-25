using GreenCoinHealth.Server.Data;
using GreenCoinHealth.Server.Models;
using GreenCoinHealth.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenCoinHealth.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Recovery_PasswordController : ControllerBase
    {
        private readonly GreenCoinHealthContext _context;

        public Recovery_PasswordController(GreenCoinHealthContext context)
        {
            _context = context;
        }

        [HttpPost("Recovery-password")]
        public IActionResult recoveryPassword([FromBody] MailDTO Email_Addressee)
        {
            var us = new UserServices();
            if (Email_Addressee == null)
            {
                return BadRequest("El correo no puede estar vacío");
            }
            var resultado = us.ServiceMail(Email_Addressee);
            return StatusCode(resultado.Code, resultado);
        }
    }
}
