using System.Text.Json;
namespace Common.Utils
{
    public static class DtoUtils
    {

        public static List<string> GetNonNullFields<T>(T dto, params string[] excludeFields)
        {
            return typeof(T)
                .GetProperties()
                .Where(p => p.GetValue(dto) != null && (excludeFields != null ? !excludeFields.Contains(p.Name) : true))
                .Select(p => p.Name)
                .ToList();
        }

        public static string GetSelectedFieldsAsJson<T>(T dto, params string[] includeFields)
        {
            var dict = typeof(T)
                .GetProperties()
                .Where(p => includeFields.Contains(p.Name))
                .Select(p => new { p.Name, Value = p.GetValue(dto) })
                .Where(p => p.Value != null)
                .ToDictionary(p => p.Name, p => p.Value);

            return JsonSerializer.Serialize(dict);
        }
    }
}
