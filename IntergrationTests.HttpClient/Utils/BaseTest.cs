using IntegrationTests.Helpers;

namespace IntegrationTests.Utils
{ 
    internal class BaseTest
    {
        [SetUp]
        protected void CreateClient()
        {
            Client = WebAppFactory.SetupClient();
            RequestHelper = new RequestHelper(Client);
        }

        public HttpClient Client { get; set; }

        public RequestHelper RequestHelper { get; set; }
    }
}
