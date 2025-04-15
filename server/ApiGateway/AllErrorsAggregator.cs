using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net;
using System.Text;

namespace ApiGateway
{
    public class AllErrorsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var combined = new Dictionary<string, object>();

            foreach (var context in responses)
            {
                var routeKey = context.Items.DownstreamRoute().Key;
                var serviceName = routeKey.Replace("-service-errors", "");
                var content = await context.Items.DownstreamResponse().Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject(content);
                if (deserialized != null)
                { 
                    combined.Add(serviceName, deserialized);
                }
            }

            // Crear el contenido con el tipo correcto
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(combined),
                Encoding.UTF8,
                "application/json"  // ← Esto establece el Content-Type
            );

            var headers = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new ("Content-Type", new List<string> { "application/json" })
            };

            return new DownstreamResponse(
                jsonContent,
                HttpStatusCode.OK,
                headers,
                "OK");
        }
    }
}
