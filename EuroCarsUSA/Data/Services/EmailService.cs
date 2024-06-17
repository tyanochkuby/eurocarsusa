using System.Net.Mail;
namespace EuroCarsUSA.Data.Services
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(string to, string subject, string body)
        {
            string email = _configuration["EmailSettings:Email"];
            string password = _configuration["EmailSettings:Password"];

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
