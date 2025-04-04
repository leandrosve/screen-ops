using MoviesService.Dtos;
using MoviesService.Models;
using System.Text.Json;

namespace MoviesService.Utils
{
    public class JsonLoader
    {

        public static List<CountryDto> LoadCountries()
        {
            var jsonPath = Path.Combine("Static", "countries.json");
            string json = File.ReadAllText(jsonPath);
            JsonDocument doc = JsonDocument.Parse(json);

            var countries = new List<CountryDto>();
            int id = 1;
            foreach (var element in doc.RootElement.EnumerateArray())
            {
                string? name = element.GetProperty("name").GetString();
                string? cca2 = element.GetProperty("cca2").GetString();
                
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(cca2))
                {
                    throw new ArgumentException("Error while loading countries");
                }

                countries.Add(new CountryDto { Code = cca2, Name = name });
                id++;
            }
            return countries;
        }

        public static List<LanguageDto> LoadLanguages()
        {
            var jsonPath = Path.Combine("Static", "languages.json");
            string json = File.ReadAllText(jsonPath);
            JsonDocument doc = JsonDocument.Parse(json);

            var languages = new List<LanguageDto>();
            int id = 1;
            foreach (var element in doc.RootElement.EnumerateArray())
            {
                string? name = element.GetProperty("name").GetString();
                string? code = element.GetProperty("code").GetString();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(code))
                {
                    throw new ArgumentException("Error while loading languages");
                }

                languages.Add(new LanguageDto { Code = code, Name = name });
                id++;
            }
            return languages;
        }

        public static List<Genre> LoadGenres()
        {
            var jsonPath = Path.Combine("Static", "genres.json");
            string json = File.ReadAllText(jsonPath);
            JsonDocument doc = JsonDocument.Parse(json);

            var genres = new List<Genre>();
            foreach (var element in doc.RootElement.EnumerateArray())
            {
                string? name = element.GetProperty("name").GetString();
                int? id = element.GetProperty("id").GetUInt16();

                if (string.IsNullOrWhiteSpace(name) || id == null)
                {
                    throw new ArgumentException("Error while loading languages");
                }

                genres.Add(new Genre { Name = name, Id = id.Value });
            }
            return genres;
        }
    }
}
