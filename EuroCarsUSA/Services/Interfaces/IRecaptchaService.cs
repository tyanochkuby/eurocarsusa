namespace EuroCarsUSA.Services.Interfaces;

public interface IRecaptchaService
{
    Task<bool> IsReCaptchaValid(string recaptchaResponse);
}
