using Microsoft.Extensions.Localization;

namespace EuroCarsUSA.Extensions
{
    public static class EnumHelper
    {
        public static List<T> GetEnumListFromString<T>(string s) where T : Enum => !string.IsNullOrEmpty(s) ?
                    Enum.GetValues(typeof(T))
                        .Cast<T>()
                        .Where(m => s.ToLower().Split(new[] { ' ', ',' }).Any(s => s == m.ToString().ToLower()))
                        .ToList()
                    : new List<T>();
        
        public static string ToLocalizedString<T>(this T enumValue, IStringLocalizer localizer) where T : Enum
        {
            string enumKey = $"{typeof(T).Name}_{enumValue}";
            var translation = localizer[enumKey];
            return translation.Value;
        }
    }
}
