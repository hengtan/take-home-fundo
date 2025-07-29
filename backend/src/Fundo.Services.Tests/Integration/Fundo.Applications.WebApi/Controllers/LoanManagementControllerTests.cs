using System.Net.Http;
using System.Threading.Tasks;
using Fundo.Applications.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Fundo.Services.Tests.Integration.Fundo.Applications.WebApi.Controllers
{
    public class LoanManagementControllerTests(WebApplicationFactory<global::Fundo.Applications.WebApi.Startup> factory)
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        [Fact]
        public async Task GetBalances_ShouldReturnExpectedResult()
        {
            var response = await _client.GetAsync("/loan");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
