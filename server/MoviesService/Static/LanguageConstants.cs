using MoviesService.Dtos;
using MoviesService.Utils;

namespace MoviesService.Static
{
    public static class LanguageConstants
    {
        public static Dictionary<string, LanguageDto> Languages { get; private set; } = new();

        public static LanguageDto? GetByCode(string languageCode)
        {
            LanguageDto? value;
            return Languages.TryGetValue(languageCode, out value) ? value : null;
        }


        // Call on startup
        public static void Load()
        {
            var languages = JsonLoader.LoadLanguages();
            Languages = languages.ToDictionary(l => l.Code,  l => new LanguageDto { Code = l.Code, Name = l.Name });
        }

    }
}
