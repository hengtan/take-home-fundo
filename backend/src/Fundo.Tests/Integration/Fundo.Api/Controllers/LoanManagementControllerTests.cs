using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Fundo.Application.Errors.ErrorsMessages;
using Fundo.Services.Tests.Integration.Common;
using Fundo.Tests.Infrastructure;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace Fundo.Tests.Integration.Fundo.Api.Controllers;

public class LoanManagementControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public LoanManagementControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Balances_Should_Return_Expected_Result()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        var response = await _client.GetAsync("/loans");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_Loan_Should_Return_Created_When_Request_Is_Valid()
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
    public async Task Post_Loan_Should_Return_Bad_Request_When_Amount_Is_Negative()
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
    public async Task Post_Loan_Should_Return_Bad_Request_When_Applicant_Name_Is_Null()
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
    public async Task Post_Loan_Should_Return_Bad_Request_When_Body_Is_Empty()
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

    [Fact]
    public async Task Get_Loan_By_Id_Should_Return_Loan_When_Id_Is_Valid()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        var request = new
        {
            amount = 1000m,
            currentBalance = 1000m,
            applicantName = "Integration User"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        var createResponse = await _client.PostAsync("/loans", content);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createResponseBody = await createResponse.Content.ReadAsStringAsync();
        var createdLoanId = JsonDocument
            .Parse(createResponseBody)
            .RootElement
            .GetGuid();

        var getResponse = await _client.GetAsync($"/loans/{createdLoanId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var getResponseBody = await getResponse.Content.ReadAsStringAsync();
        using var json = JsonDocument.Parse(getResponseBody);
        var loan = json.RootElement;

        loan.GetProperty("id").GetGuid().Should().Be(createdLoanId);
        loan.GetProperty("applicantName").GetString().Should().Be(request.applicantName);
        loan.GetProperty("amount").GetDecimal().Should().Be(request.amount);
        loan.GetProperty("currentBalance").GetDecimal().Should().Be(request.currentBalance);
    }

    [Fact]
    public async Task Get_Loan_By_Id_Should_Return_NotFound_When_Id_Is_Invalid()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        var nonExistentLoanId = Guid.NewGuid();

        var response = await _client.GetAsync($"/loans/{nonExistentLoanId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseBody = await response.Content.ReadAsStringAsync();
        using var json = JsonDocument.Parse(responseBody);
        var root = json.RootElement;

        root.TryGetProperty("error", out var errorProp).Should().BeTrue("Expected 'error' property in response.");
        errorProp.GetString().Should().Contain("was not found");
    }

    [Fact]
    public async Task Get_Loan_By_Id_Should_Return_NotFound_When_Id_Is_Not_Guid()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        const string invalidId = "not-a-guid";

        var response = await _client.GetAsync($"/loans/{invalidId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAll_ShouldReturnLoans_WhenExists()
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
        var postResponse = await _client.PostAsync("/loans", content);
        postResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = await _client.GetAsync("/loans");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadAsStringAsync();

        using var json = JsonDocument.Parse(body);
        json.RootElement.ValueKind.Should().Be(JsonValueKind.Array);
        json.RootElement.GetArrayLength().Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetAll_Should_Return_Correct_Status_Based_On_Data()
    {
        if (!DatabaseHelper.IsDatabaseAvailable())
            return;

        var response = await _client.GetAsync("/loans");

        if (await DatabaseHelper.HasAnyLoanAsync(_factory))
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        else
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}