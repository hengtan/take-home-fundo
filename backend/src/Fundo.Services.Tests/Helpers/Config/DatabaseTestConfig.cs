using System;
using Microsoft.Extensions.Configuration;

namespace Fundo.Services.Tests.Helpers.Config;

public static class DatabaseTestConfig
{
    private static readonly IConfigurationRoot _config;

    static DatabaseTestConfig()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.test.json", optional: false)
            .Build();
    }

    public static string ConnectionString =>
        _config.GetConnectionString("Default")!;
}