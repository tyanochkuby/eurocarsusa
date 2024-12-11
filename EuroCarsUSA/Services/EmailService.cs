using System.Net.Mail;
using EuroCarsUSA.Extensions;
using EuroCarsUSA.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EuroCarsUSA.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public string AdminEmail { get; init; }

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            AdminEmail = _emailSettings.AdminEmail;
        }

        public async Task SendEmail(string to, string subject, string body)
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
            smtp.Host = "smtp.webio.pl"; // Webio SMTP host
            smtp.Port = 587; // Webio SMTP port
            smtp.Credentials = new System.Net.NetworkCredential(email, password);
            smtp.EnableSsl = true;

            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send email.", ex);
            }
        }
    }
}
