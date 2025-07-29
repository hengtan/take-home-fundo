using Fundo.Services.Tests.Helpers.Config;
using Microsoft.Data.SqlClient;

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
}