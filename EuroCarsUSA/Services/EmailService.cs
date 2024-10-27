using System.Net.Mail;
using EuroCarsUSA.Extensions;
using EuroCarsUSA.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EuroCarsUSA.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public void SendEmail(string to, string subject, string body)
        {
            string email = _emailSettings.Email;
            string password = _emailSettings.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new Exception("Email and password are not set in environment variables.");

            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(email);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-mail.outlook.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(email, password);
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch
            {
                throw;
            }
        }
    }
}
