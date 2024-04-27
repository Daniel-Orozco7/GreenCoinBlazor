using GreenCoinHealth.Server.Models;
using GreenCoinHealth.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;

public class UserServices
{
    public ResultDTO RegisterUser(UserDTO user)
    {
        var respuestaJson = new ResultDTO()
        {
            Mensaje = "El DNI ya se encuentra Registrado",
            Code = 400
        };

        if (UserRepository.RegisterUser(user))
        {
            respuestaJson = new ResultDTO()
            {
                Mensaje = "Usuario registrado correctamente",
                Code = 201
            };

        }
        else
        {
            respuestaJson = new ResultDTO()
            {
                Mensaje = "Se presentó un inconveniente al registrar el usuario",
                Code = 500
            };
        }

        return respuestaJson;
    }
    public ResultDTO LogingUser(LoginDTO model)
    {
        var respuestaJson = new ResultDTO()
        {
            Mensaje = "Credenciales incorrectas ",
            Code = 401
        };

        if (UserRepository.Login(model.Email, model.Password))
        {
            respuestaJson = new ResultDTO()
            {
                Mensaje = "Inicio de sesion correcto",
                Code = 200
            };
        }

        return respuestaJson;
    }
    public ResultDTO ServiceMail(MailDTO Email_Addressee)
    {

        var respuestaJson = new ResultDTO()
        {
            Mensaje = "Error al enviar el correo",
            Code = 400
        };

        if (UserRepository.Mail(Email_Addressee))
        {
            respuestaJson = new ResultDTO()
            {
                Mensaje = "Correo enviado exitosamente",
                Code = 200
            };
        }

        return respuestaJson;
    }

    public ResultDTO chagePassword(ChagePasswordDTO model)
    {
        var respuestaJson = new ResultDTO()
        {
            Mensaje = "Error al cambiar la contraseña",
            Code = 400
        };
        if(UserRepository.chagePassword(model))
        {
            respuestaJson = new ResultDTO()
            {
                Mensaje = "Contraseña cambiada correctamente",
                Code = 200
            };
        }
        return respuestaJson;
    }

    public List<object> ListUser(List<object> users)
    {
        if(users != null && users.Any())
        {
            return users;
        }
        else
        {
            throw new InvalidOperationException("No hay usuarios registrados");
        }
    }

    public UserDTO FindByDni(string dni)
    {
        return UserRepository.FindByDni(dni);
    }
}