using IntegrationTests.Helpers;
using NUnit.Framework;
using System.Net.Http;

namespace IntegrationTests.Utils
{
    internal class BaseTest
    {
        [SetUp]
        protected void CreateClient()
        {
            Client = WebAppFactory.SetupClient();
        }

        public HttpClient Client { get; set; }
    }
}
