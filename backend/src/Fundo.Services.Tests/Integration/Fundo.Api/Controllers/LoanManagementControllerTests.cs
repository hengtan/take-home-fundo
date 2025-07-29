using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Fundo.Services.Tests.Integration.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Fundo.Services.Tests.Integration.Fundo.Api.Controllers;

public class LoanManagementControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false
    });

    [Fact]
    public async Task GetBalances_ShouldReturnExpectedResult()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        JwtTestContext.AttachToken(_client);

        var response = await _client.GetAsync("/loans");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostLoan_ShouldReturn_Created_WhenRequestIsValid()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        JwtTestContext.AttachToken(_client);

        var loan = new
        {
            amount = 1000m,
            currentBalance = 1000m,
            applicantName = "Integration User"
        };

        var content = new StringContent(JsonSerializer.Serialize(loan), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/loans", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK); // ou Created
    }
}