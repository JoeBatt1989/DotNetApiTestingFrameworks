using BoDi;
using IntegrationTests.Specflow.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IntegrationTests.Utils
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario]
        public void CreateClient(IObjectContainer container)
        {
            HttpClient Client = WebAppFactory.SetupClient();
            container.RegisterInstanceAs(new RequestHelper(Client));
        }
    }

}
