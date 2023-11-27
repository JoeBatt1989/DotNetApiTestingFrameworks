using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WebAPI;

namespace IntegrationTests.Specflow.Helpers
{
    public static class WebAppFactory
    {
        public static HttpClient SetupClient()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = "Test";
                            options.DefaultChallengeScheme = "Test";
                            options.DefaultScheme = "Test";
                        })
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                    });
                });

            return webApplicationFactory.CreateClient();
        }
    }
}
