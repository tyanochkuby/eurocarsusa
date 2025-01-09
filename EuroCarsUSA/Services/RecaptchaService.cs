using EuroCarsUSA.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace EuroCarsUSA.Services;

public class RecaptchaService : IRecaptchaService
{
    private readonly string _recaptchaSecret;

    public RecaptchaService(IConfiguration configuration)
    {
        _recaptchaSecret = configuration["CaptchaSecretKey"];
    }

    public async Task<bool> IsReCaptchaValid(string recaptchaResponse)

    {
        using (var client = new HttpClient())
        {
            var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_recaptchaSecret}&response={recaptchaResponse}", null);
            var jsonString = await response.Content.ReadAsStringAsync();
            dynamic jsonData = JObject.Parse(jsonString);
            return jsonData.success == "true";
        }
    }
}
