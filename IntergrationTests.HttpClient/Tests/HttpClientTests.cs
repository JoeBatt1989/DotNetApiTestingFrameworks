using FluentAssertions;
using IntegrationTests.Utils;
using Snapshooter.NUnit;
using System.Net;
using WebAPI.Models;

namespace IntegrationTests.Tests
{
    internal class HttpClientTests : BaseTest
    {
        [Test]
        public async Task Get_Client_HttpClient()
        {
            this.RequestHelper.SetUrl("https://localhost:44387/api/client")
                .SetRequestVerb("GET");

            var response = this.RequestHelper.GetResponseAsync().Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Snapshot.Match(ResponseFormatter.FormatResponse(response.Content.ReadAsStringAsync().Result));
        }

        [Test]
        public async Task Post_Client_HttpClient()
        {
            // This is an example and data should be held in a data script
            var clientRequest = new Client()
            {
                Title = "Mr",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@test.com",
                ContactDetails = new ContactDetails()
                {
                    PhoneNumber = "07959789876",
                    PhoneType = "Mobile"
                }
            };

            this.RequestHelper.SetUrl("https://localhost:44387/api/client")
                .SetRequestVerb("POST");

            this.RequestHelper.Request.AddJsonBody<Client>(clientRequest);

            var response = this.RequestHelper.GetResponseAsync().Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // DB assertion goes here
        }
    }
}
