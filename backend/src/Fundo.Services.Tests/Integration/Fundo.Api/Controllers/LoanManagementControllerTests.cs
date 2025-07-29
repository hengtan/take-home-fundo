using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Fundo.Application.Errors.ErrorsMessages;
using Fundo.Services.Tests.Integration.Common;
using Fundo.Services.Tests.Infrastructure;
using Xunit;

namespace Fundo.Services.Tests.Integration.Fundo.Api.Controllers;

public class LoanManagementControllerTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();


    [Fact]
    public async Task GetBalances_ShouldReturnExpectedResult()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        var response = await _client.GetAsync("/loans");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostLoan_ShouldReturn_Created_WhenRequestIsValid()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        var loan = new
        {
            amount = 1000m,
            currentBalance = 1000m,
            applicantName = "Integration User"
        };

        var content = new StringContent(JsonSerializer.Serialize(loan), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/loans", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostLoan_ShouldReturn_BadRequest_When_Amount_Is_Negative()
    {
        // Arrange
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        var loan = new
        {
            amount = -1000m,
            currentBalance = 1000m,
            applicantName = "User Negative Amount"
        };

        var content = new StringContent(JsonSerializer.Serialize(loan), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/loans", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        responseBody.Should().Contain(ErrorMessages.LoanAmountAndBalanceMustBePositive);
    }

    [Fact]
    public async Task PostLoan_ShouldReturn_BadRequest_WhenApplicantNameIsNull()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        var loan = new
        {
            amount = 1000m,
            currentBalance = 1000m,
            applicantName = (string?)null
        };

        var content = new StringContent(JsonSerializer.Serialize(loan), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/loans", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        using var json = JsonDocument.Parse(responseBody);
        var root = json.RootElement;

        root.GetProperty("errors").TryGetProperty("ApplicantName", out var applicantErrors).Should().BeTrue("Expected ApplicantName validation error");
        applicantErrors[0].GetString().Should().Be("The ApplicantName field is required.");
    }

    [Fact]
    public async Task PostLoan_ShouldReturn_BadRequest_WhenBodyIsEmpty()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        var content = new StringContent("", Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/loans", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        using var json = JsonDocument.Parse(responseBody);
        var root = json.RootElement;

        var errors = root.GetProperty("errors");

        errors.TryGetProperty("", out var bodyErrors).Should().BeTrue("Expected body error for empty payload");
        bodyErrors[0].GetString().Should().Be("A non-empty request body is required.");

        errors.TryGetProperty("command", out var commandErrors).Should().BeTrue("Expected error for missing 'command'");
        commandErrors[0].GetString().Should().Be("The command field is required.");
    }
}