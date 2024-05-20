using GreenCoinHealth.Server.Data;
using GreenCoinHealth.Server.Models;
using GreenCoinHealth.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace GreenCoinHealth.Server.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly GreenCoinHealthContext _context;
        private readonly UserRepository _userRepository;

        public UserController(GreenCoinHealthContext context, UserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpPost("send-token")]
        public IActionResult SendToken([FromBody] string email)
        {
            var token = _userRepository.GenerateJWTToken(email);
            if (token == null)
            {
                return BadRequest(new { Mensaje = "El correo no se encuentra registrado" });
            }
            else
            {
                _userRepository.SendtokenEmail(email, token);
                return Ok(new { Mensaje = "Token enviado correctamente" });
            }
        }
        [HttpPost("validate-token")]
        public async Task<IActionResult> ValidateToken([FromBody] string Token)
        {
            if(_userRepository.ValidateToken(Token, out string email))
            {
                // Crear claims basado en la información del usuario
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email)
                // Puedes añadir más claims según sea necesario
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Autenticar al usuario
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return Ok(new { Mensaje = "Token valido" });
            }
            else
            {
                return Unauthorized("Token invalido");
            }
        }
        //POST: api/User
        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser([FromBody] UserDTO user)
        {
            var us = new UserServices();
            if(user == null)
            {
                return BadRequest("Los datos del usuario no pueden estar vacíos");
            }
            var resultado = us.RegisterUser(user);
            return StatusCode(resultado.Code, resultado);
        }

        [HttpPost("ChangePassword")]
        public IActionResult changePassword([FromBody] ChagePasswordDTO model)
        {
            var us = new UserServices();
            if (model == null)
            {
                return BadRequest("Los datos no pueden estar vacíos");
            }
            var resultado = us.chagePassword(model);
            return StatusCode(resultado.Code, resultado);
        }
        [HttpGet("ReadUsers")]
        public ActionResult<List<UserDTO>> ReadUsers()
        {
            try
            {

                var users = _userRepository.ReadUsers();

                if (users != null)
                {
                    return users;// Ok(new { StatusCode = 200, Usuarios = users });
                }
                else
                {
                    return StatusCode(500, "No se pudo obtener la lista de usuarios");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> updateUser([FromBody] UserDTO user)
        {
            var user_Exists = await _context.Users.FirstOrDefaultAsync(u => u.Dni == user.Dni);
            

            if (user_Exists == null)
            {
                return NotFound(new {Mensaje= "El usuario no existe" });
            }
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            user_Exists.Name = user.Name;
            user_Exists.LastName = user.LastName;
            user_Exists.Username = user.username;
            user_Exists.Email = user.Email;
            user_Exists.Phone = user.Phone;
            user_Exists.IdGender = user.Gender;
            user_Exists.IdRole = user.UserRole;

            _context.Users.Update(user_Exists);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Usuario actualizado correctamente" });

        }

        [HttpDelete("DeleteUser/{dni}")]
        public async Task<IActionResult> deleteUSer(string dni)
        {
            var us = await _context.Users.FirstOrDefaultAsync(u => u.Dni == dni);

            if(us == null)
            {
                return NotFound(new { Mensaje = "El usuario no existe" });
            }
            _context.Users.Remove(us);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Usuario eliminado correctamente" });
        }

        [HttpGet("GetUser/{dni}")]
        public IActionResult GetUser(string dni)
        {
            var us = new UserServices();

            var existingUser =  us.FindByDni(dni);

            if (existingUser == null)
            {
                return NotFound(new { Mensaje = "El usuario con DNI "+ dni + " no se encuentra en la base datos " });
            }

            return StatusCode(StatusCodes.Status200OK, existingUser);
        }
        
    }
}
