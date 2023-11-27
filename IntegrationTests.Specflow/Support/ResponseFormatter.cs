using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntegrationTests.Utils
{
    static class ResponseFormatter
    {
        public static string FormatResponse(string response)
        {
            return JToken.Parse(response).ToString(Formatting.Indented);
        }
    }
}
