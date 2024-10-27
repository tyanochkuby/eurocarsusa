using EuroCarsUSA.Extensions;
using EuroCarsUSA.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EuroCarsUSA.Services
{
    public class CookieService : ICookieService
    {
        IHttpContextAccessor _httpContextAccessor;
        CookieOptions _cookieOptions = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(14) };
        CookieNames _cookieNames;

        public CookieService(IHttpContextAccessor httpContextAccessor, IOptions<CookieNames> cookieNames)
        {
            _httpContextAccessor = httpContextAccessor;
            _cookieNames = cookieNames.Value;
        }

        public List<Guid> GetUserLikedCars()
        {
            var likes = _httpContextAccessor.HttpContext.Request.Cookies[_cookieNames.Likes];
            return string.IsNullOrEmpty(likes) ? new List<Guid>() : JsonConvert.DeserializeObject<List<Guid>>(likes);
        }

        public List<Guid> GetUserViewedCars()
        {
            var views = _httpContextAccessor.HttpContext.Request.Cookies[_cookieNames.Views];
            return string.IsNullOrEmpty(views) ? new List<Guid>() : JsonConvert.DeserializeObject<List<Guid>>(views);
        }

        public void UpdateUserLikedCars(List<Guid> carsIds)
        {
            lock (_httpContextAccessor.HttpContext)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append(_cookieNames.Likes, JsonConvert.SerializeObject(carsIds), _cookieOptions);
            }
        }

        public void UpdateUserViewedCars(List<Guid> carsIds)
        {
            lock (_httpContextAccessor.HttpContext)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append(_cookieNames.Views, JsonConvert.SerializeObject(carsIds), _cookieOptions);
            }
        }
    }
}
