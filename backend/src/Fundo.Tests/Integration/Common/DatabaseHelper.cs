using System.Threading.Tasks;
using Fundo.Infrastructure.Persistence;
using Fundo.Services.Tests.Helpers.Config;
using Fundo.Tests.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Fundo.Services.Tests.Integration.Common;

public static class DatabaseHelper
{
    public static bool IsDatabaseAvailable()
    {
        var connection = DatabaseTestConfig.ConnectionString;

        try
        {
            using var conn = new SqlConnection(connection);
            conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static async Task<bool> HasAnyLoanAsync(CustomWebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LoanDbContext>();

        return await context.Loans.AnyAsync();
    }
}