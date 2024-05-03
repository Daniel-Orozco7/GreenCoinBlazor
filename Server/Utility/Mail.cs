using GreenCoinHealth.Server.Utilities;
using GreenCoinHealth.Server.Models;
using System.Net.Mail;
using GreenCoinHealth.Server.Data;

namespace GreenCoinHealth.Server.Utilities
{
    public class Mail
    {
        public bool SendMail(string correo, string asunto, string mensaje)
        {
            EncriptPwd enc = new EncriptPwd();
            bool result = false;

            try
            {
                using (var context = new GreenCoinHealthContext())
                {
                    var us = context.Users.FirstOrDefault(u => u.Email == correo);

                    if (us != null)
                    {
                        context.SaveChanges();
                        MailMessage mail = new MailMessage();
                        mail.To.Add(correo);
                        mail.From = new MailAddress("greencoinhealth@gmail.com");
                        mail.Subject = asunto;
                        mail.Body = mensaje;
                        mail.IsBodyHtml = true;

                        var smtp = new SmtpClient("")
                        {
                            Credentials = new System.Net.NetworkCredential("greencoinhealth@gmail.com", "atlrpgqzegshhgqp"),
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true
                        };
                        smtp.Send(mail);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
            return result;
        }
    }
}
