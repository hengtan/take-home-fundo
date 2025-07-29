using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fundo.Services.Tests.Infrastructure;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.test.json");
            config.AddJsonFile(path, optional: false);
        });

        builder.ConfigureServices(services =>
        {
            // Remove autenticações reais
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(AuthenticationSchemeProvider));

            if (descriptor != null)
                services.Remove(descriptor);

            // Adiciona esquema fake
            services.AddAuthentication("TestScheme")
                .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>(
                    "TestScheme", options => { });

            // Garante autenticação nos testes
            services.PostConfigureAll<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = "TestScheme";
                options.DefaultChallengeScheme = "TestScheme";
            });
        });
    }
}