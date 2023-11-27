using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Utils
{
    public class RequestHelper
    {
        public RequestHelper(HttpClient client)
        {
            this.Client = client;
        }

        public HttpResponseMessage Response { get; private set; }

        public HttpRequestMessage Request { get; set; }

        public HttpClient Client { get; }

        public RequestHelper SetRequestVerb(string httpMethod)
        {
            this.Request.Method = httpMethod.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "PUT" => HttpMethod.Put,
                "PATCH" => HttpMethod.Patch,
                "POST" => HttpMethod.Post,
                "DELETE" => HttpMethod.Delete,
                _ => throw new Exception($"Unsupported request method: {httpMethod}. Please review your test scenario."),
            };

            return this;
        }

        public async Task<HttpResponseMessage> GetResponseAsync()
        {
            var request = this.Request.Clone();

            return this.Response = await this.Client.SendAsync(request);
        }

        public RequestHelper SetBasicAuth(string username, string password)
        {
            string svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));

            this.Request.Headers.Add("Authorization", "Basic " + svcCredentials);

            return this;
        }

        public RequestHelper SetUrl(string url)
        {
            this.Request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{url}"),
            };

            return this;
        }
    }
}

