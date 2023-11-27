using FluentAssertions;
using IntegrationTests.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Snapshooter.NUnit;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace ApiProjectTemplate.StepDefintions
{
    [Binding]
    internal class ApiTestExampleSteps
    {
        public ApiTestExampleSteps(RequestHelper requestHelper) 
        { 
            RequestHelper = requestHelper;
        }

        private RequestHelper RequestHelper { get; set; }

        [Given(@"I have an endpoint (.*)")]
        public void GivenIHaveAnEndpoint(string endpoint) => this.RequestHelper.SetUrl($"https://localhost:51326/{endpoint}");

        [When(@"I send a (.*) request")]
        public void WhenISendARequest(string verb)
        {
            this.RequestHelper.SetRequestVerb(verb);
            _ = this.RequestHelper.GetResponseAsync().Result;
        }

        [Then(@"I will get a (.*) response")]
        public void ThenIWillGetAResponse(string expectedCode)
        {
            Enum.TryParse(expectedCode, out HttpStatusCode status);
            this.RequestHelper.Response.StatusCode.Should().Be(status);
        }

        [Then(@"the expected reponse is returned")]
        public void ThenTheExpectedReponseIsReturned()
        {
            var result = this.RequestHelper.Response.Content.ReadAsStringAsync().Result;
            var formattedResult = JToken.Parse(result).ToString(Formatting.Indented);
            Snapshot.Match(formattedResult);
        }
    }
}

