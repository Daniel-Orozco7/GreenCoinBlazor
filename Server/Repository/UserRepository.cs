﻿
using GreenCoinHealth.Server.Utilities;
using GreenCoinHealth.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using GreenCoinHealth.Shared;
using GreenCoinHealth.Server.Data;

public class UserRepository
{
    public string validate_user(UserDTO user)
    {
        if (string.IsNullOrWhiteSpace(user.Name))
        {
            return "El campo nombre es obligatorio";
        }
        if (user.Name.Length <= 3)
        {
            return "El campo nombre debe tener al menos 3 caracteres";
        }
        if (!string.IsNullOrWhiteSpace(user.second_name))
        {
            return "El campo segundo nombre es obligatorio";
        }
        if (string.IsNullOrWhiteSpace(user.LastName))
        {
            return "El campo apellido es obligatorio";
        }
        if (string.IsNullOrWhiteSpace(user.Gender))
        {
            return "El campo Genero es obligatorio";
        }
        if (user.LastName.Length <= 3)
        {
            return "El campo apellido debe tener al menos 3 caracteres";
        }
        if (string.IsNullOrWhiteSpace(user.second_surname))
        {
            return "El campo segundo apellido es obligatorio";
        }
        if (string.IsNullOrWhiteSpace(user.username))
        {
            return "El campo usuario es obligatorio";
        }
        if (string.IsNullOrWhiteSpace(user.Dni))
        {
            return "El campo DNI es obligatorio";
        }
        if (string.IsNullOrWhiteSpace(user.TypeDni))
        {
            return "El campo tipo de DNI es obligatorio";
        }
        if (string.IsNullOrWhiteSpace(user.Email))
        {
            return "El campo correo electrónico es obligatorio";
        }
        if (!Regex.IsMatch(user.Email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
        {
            return "El correo electrónico no es válido";
        }
        if (string.IsNullOrWhiteSpace(user.Phone))
        {
            return "El campo teléfono es obligatorio";
        }
        if (!Regex.IsMatch(user.Phone, @"^\d+$") || user.Phone.Length < 8)
        {
            return "El teléfono no es válido";
        }
        if (string.IsNullOrWhiteSpace(user.Password))
        {
            return "El campo contraseña es obligatorio";
        }
        if (!Regex.IsMatch(user.Password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@#$%^&+=!_./])[A-Za-z\d@#$%^&+=!_./]{8,}$"))
        {
            return "La contraseña no cumple con los requisitos de seguridad";
        }
        if (string.IsNullOrWhiteSpace(user.Confirm_Password))
        {
            return "El campo confirmar contraseña es obligatorio";
        }
        if (!user.Password.Equals(user.Confirm_Password))
        {
            return "Las contraseñas no coinciden";
        }
        return null;
    }
    public static bool RegisterUser(UserDTO model)
    {
        EncriptPwd encriptPwd = new EncriptPwd();
        Mail m = new Mail();

        using (var context = new GreenCoinHealthContext())
        {
            try
            {
                var usuario = context.Users.SingleOrDefault(u => u.Dni == model.Dni);
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                string names = model.Name + " " + model.second_name;
                string lastnames = model.LastName + " " + model.second_surname;

                if (usuario == null)
                {
                    var user = new User()
                    {
                        Dni = model.Dni,
                        TypeDni = model.TypeDni,
                        Name = textInfo.ToTitleCase(names),
                        LastName = textInfo.ToTitleCase(lastnames),
                        Username = model.username,
                        Email = model.Email,
                        Phone = model.Phone,
                        Password = encriptPwd.EncryptPassword(model.Password),
                        IdGender = model.Gender,
                        IdRole = "2"
                    };
                    context.Users.Add(user);
                    context.SaveChanges();

                    string asunto = "Bienvenido/a a GreenCoin Health";
                    string mensaje = @"
                    <html>
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                            }
                            .container {
                                background-color: #fff;
                                width: 80%;
                                margin: 20px auto;
                                padding: 20px;
                                box-shadow: 0 0 10px rgba(0,0,0,0.1);
                            }
                            h1 {
                                color: #333;
                            }
                            p {
                                color: #666;
                                font-size: 16px;
                                line-height: 1.5;
                            }
                            .footer {
                                text-align: center;
                                font-size: 12px;
                                color: #999;
                                margin-top: 20px;
                            }
                        </style>
                    </head>
                <body>
                <div class=""container"">
                    <h1>¡Bienvenido/a a GreenCoin Health!</h1>
                    <p>Hola<strong>{name}</strong></p>
                    <p><strong>Bienvenido a GreenCoin Health:</strong> Tu compañero en el camino hacia una vida saludable y sostenible.</p>
                    <p>Te damos la más cordial bienvenida a GreenCoin Health, donde te acompañaremos en tu viaje hacia un estilo de vida más saludable y sostenible.</p>
                    <p>Nuestra aplicación te permite realizar un seguimiento de tu ingesta diaria de alimentos, acceder a contenido educativo sobre nutrición, gestionar tus finanzas, y lo más importante, vincular la información nutricional con la sostenibilidad.</p>
                    <p>Estamos emocionados de tener la oportunidad de ayudarte en este camino. Si necesitas ayuda o tienes alguna pregunta, no dudes en contactarnos.</p>
                    <p>¡Gracias por unirte a nosotros en este emocionante viaje hacia un futuro más saludable y sostenible!</p>
                    <div class=""footer"">
                        <p>Este es un correo automático, por favor no responder a este mensaje.</p>
                    </div>
                </div>
            </body>
        </html>";

                    mensaje = mensaje.Replace("[name]", model.Name);
                    m.SendMail(model.Email, asunto, mensaje);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el usuario: {ex.Message}");
                return false;
            }
        }
    }

    public static bool Login(string email, string password)
    {
        EncriptPwd encriptPwd = new EncriptPwd();
        using (var context = new GreenCoinHealthContext())
        {
            try
            {
                var user = context.Users.SingleOrDefault(u => u.Email == email && u.Password == encriptPwd.EncryptPassword(password));
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
    public static bool Mail(MailDTO model)
    {
        Mail m = new Mail();
        bool result = false;
        using (var context = new GreenCoinHealthContext())
        {
            try
            {
                var us = context.Users.FirstOrDefault(u => u.Email == model.Email_Addressee);
                if (us != null)
                {
                    string asunto = "Recuperación de contraseña";
                    string mensaje = @"<html>
                                        <head>
                                            <meta charset=""UTF-8"">
                                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                            <style>
                                                body {
                                                    font-family: Arial, sans-serif;
                                                    background-color: #f4f4f4;
                                                    margin: 0;
                                                    padding: 0;
                                                }
                                                .container {
                                                    background-color: #fff;
                                                    width: 80%;
                                                    margin: 20px auto;
                                                    padding: 20px;
                                                    box-shadow: 0 0 10px rgba(0,0,0,0.1);
                                                }
                                                h1 {
                                                    color: #333;
                                                }
                                                p {
                                                    color: #666;
                                                    font-size: 16px;
                                                }
                                                .footer {
                                                    text-align: center;
                                                    font-size: 12px;
                                                    color: #999;
                                                    margin-top: 20px;
                                                }
                                            </style>
                                        </head>
                                    <body>
                                    <div class=""container"">
                                        <h1>Recuperación de Contraseña</h1>
                                        <p>Hola,</p>
                                        <p>Has solicitado recuperar tu contraseña para tu cuenta. Tu contraseña actual es:</p>
                                        <p><strong>{Password}</strong></p>
                                        <p>Por razones de seguridad, te recomendamos cambiar tu contraseña inmediatamente después de iniciar sesión.</p>
                                        <div class=""footer"">
                                            <p>Este es un correo automático, por favor no responder a este mensaje.</p>
                                        </div>
                                    </div>
                                </body>
                            </html>";
                    mensaje = mensaje.Replace("[Password]", us.Password);
                    m.SendMail(model.Email_Addressee, asunto, mensaje);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine($"Error al recuperar la contraseña: {ex.Message}");
            }
            return result;
        }
    }
    public static bool chagePassword(ChagePasswordDTO model)
    {
        EncriptPwd encriptPwd = new EncriptPwd();
        using (var context = new GreenCoinHealthContext())
        {
            try
            {
                var user = context.Users.SingleOrDefault(u => u.Username == model.username);
                if (user != null)
                {
                    user.Password = encriptPwd.EncryptPassword(model.password);
                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar la contraseña: {ex.Message}");
                return false;
            }
        }
    }

    public List<UserDTO>? ReadUsers()
    {
        using (var context = new GreenCoinHealthContext())
        {
            try
            {
                var users = (from usr in context.Users
                             select new UserDTO
                             {
                                 Dni = usr.Dni
                                 ,TypeDni = usr.TypeDni
                                 ,Name = usr.Name
                                 ,LastName = usr.LastName
                                 ,username = usr.Username
                                 ,Phone = usr.Phone
                                 ,Email = usr.Email
                             }).ToList();

                return users;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer los usuarios: {ex.Message}");
                return null;
            }
        }
    }

    /// <summary>
    /// Encuentra un usuario por DNI.
    /// </summary>
    /// <param name="dni">El DNI del usuario a buscar.</param>
    /// <returns>Un UserDTO si se encuentra el usuario, null en caso contrario.</returns>

    public UserDTO FindByDni(string dni)
    {
        using (var context = new GreenCoinHealthContext())
        {
            try
            {
                var user = context.Users
                    .Where(u => u.Dni == dni)
                    .Select(u => new UserDTO
                    {
                        Dni = u.Dni,
                        TypeDni = u.TypeDni,
                        Name = u.Name,
                        LastName = u.LastName,
                        username = u.Username,
                        Email = u.Email,
                        Phone = u.Phone,
                        Gender = u.IdGender
                    }).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar el usuario: {ex.Message}");
                return null;
            }
        }
    }
}

