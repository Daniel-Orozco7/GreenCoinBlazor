using GreenCoinHealth.Server.Data;
using GreenCoinHealth.Server.Models;
using GreenCoinHealth.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GreenCoinHealth.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly GreenCoinHealthContext _context;
        
        public UserController(GreenCoinHealthContext context)
        {
            _context = context;
        }
        //POST: api/User
        [HttpPost("Register_User")]
        public IActionResult Register_User([FromBody] UserDTO user)
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

                var usR = new UserRepository();
                var us = new UserServices();

                var users = usR.ReadUsers();
                //var users = us.ListUser(user);

                if (us != null)
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

        [HttpPut("UpdateUser/{dni}")]
        public async Task<IActionResult> updateUser(string dni, [FromBody] UserDTO user)
        {
            var user_Exists = await _context.Users.FirstOrDefaultAsync(u => u.Dni == dni);
            

            if (user_Exists == null)
            {
                return NotFound(new {Mensaje= "El usuario no existe" });
            }
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string names = user.Name + " " + user.second_name;
            string lastnames = user.LastName + " " + user.second_surname;

            user_Exists.Dni = user.Dni;
            user_Exists.TypeDni = user.TypeDni;
            user_Exists.Name = names;
            user_Exists.LastName = lastnames;
            user_Exists.Username = user.username;
            user_Exists.Email = user.Email;
            user_Exists.Phone = user.Phone;
            user_Exists.IdGender = user.Gender;

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
        public async Task<IActionResult> getUser(string dni)
        {
            var us = await _context.Users.FirstOrDefaultAsync(u => u.Dni == dni);

            if (us == null)
            {
                return NotFound(new { Mensaje = "El usuario con DNI "+dni+" no se encuentra en la base datos " });
            }

            return Ok(us);
        }
        
    }
}
