namespace EuroCarsUSA.Services.Interfaces
{
    public interface IEmailService
    {
        public string AdminEmail { get; init; }
        Task SendEmail(string to, string subject, string body);
    }
}
