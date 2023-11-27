using IntegrationTests.Utils;
using NUnit.Framework;
using Snapshooter.NUnit;
using System.Threading.Tasks;
using WebAPI.Models;
using static RestAssured.Dsl;

namespace IntegrationTests
{
    internal class RestAssuredTests : BaseTest
    {

        [Test]
        public async Task Get_Client_RestAssured()
        {
            Given(Client)
                .When()
                .Get("https://localhost:51326/api/client")
                .Then()
                .StatusCode(200);
        }

        [Test]
        public async Task Post_Client_RestAssured()
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

            var response = Given(Client)
                .Body(clientRequest)
                .When()
                .Post("https://localhost:51326/api/client")
                .Then()
                .StatusCode(200)
                .Extract()
                .Body();

            // DB assertion goes here
        }

        [Test]
        public async Task Get_Client_RestAssured_Snapshot()
        {
            Snapshot.Match(
                ResponseFormatter.FormatResponse(
                    Given(Client)
                        .When()
                        .Get("https://localhost:51326/api/client")
                        .Then()
                        .AssertThat()
                        .StatusCode(200)
                        .Extract()
                        .Body())
                );
        }
    }
}