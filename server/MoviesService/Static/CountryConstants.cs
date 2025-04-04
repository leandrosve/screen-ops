using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoviesService.Dtos;
using MoviesService.Utils;

namespace MoviesService.Static
{
    public static class CountryConstants
    {
        public static Dictionary<string, CountryDto> Countries { get; private set; } = new();

        public static CountryDto? GetByCode(string code)
        {
            CountryDto? value;
            return Countries.TryGetValue(code, out value) ? value : null;
        }

        // Call on startup
        public static void Load()
        {
            var countries = JsonLoader.LoadCountries();
            Countries = countries.ToDictionary(l => l.Code,  l => new CountryDto { Code = l.Code, Name = l.Name });
        }

    }
}
