using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using GreenCoinHealth.Server.Utilities;
using GreenCoinHealth.Shared;
using GreenCoinHealth.Server.Data;
using GreenCoinHealth.Client.Pages;


namespace GreenCoinHealth.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticationController : ControllerBase
    {
        EncriptPwd encript = new EncriptPwd();
        private readonly string? secretKey;
        private readonly GreenCoinHealthContext _context;

        public AutenticationController(IConfiguration config, GreenCoinHealthContext context)
        {
            secretKey = config.GetSection("Settings").GetSection("Key").ToString();
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var user_Exist = _context.Users.FirstOrDefault(x => x.Email == request.Email);
            if (user_Exist != null)
            {
                var userRole = _context.Roles.FirstOrDefault(x => x.IdRole == user_Exist.IdRole);

                if (user_Exist.Email.ToUpper() == request.Email.ToUpper() && encript.EncryptPassword(request.Password) == user_Exist.Password)
                {
                    SessionDTO sesionDTO = new SessionDTO();
                    sesionDTO.Nombre = user_Exist.Name;
                    sesionDTO.Correo = user_Exist.Email;
                    sesionDTO.Rol = userRole == null ? "" : userRole.Name;


                    return StatusCode(StatusCodes.Status200OK, sesionDTO);
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, null);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, null);
            }
        }
    }
}
