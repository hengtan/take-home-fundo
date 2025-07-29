using Fundo.API.Extensions;
using Fundo.Application.DependencyInjection;
using Fundo.Infrastructure.DependencyInjection;
using Fundo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/fundo-api-.log", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Debug()
    .CreateLogger();

try
{
    Log.Information("Starting Fundo.API...");

    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.UseUrls("http://*:5000");

    // Substitui o logger padrão pelo Serilog
    builder.Host.UseSerilog();

    // Services
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddControllers();
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddSwaggerDocumentation();
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblyContaining<Fundo.Application.Commands.Loans.Create.CreateLoanCommand>());

    var app = builder.Build();

    // Middleware customizado (ExceptionHandling etc)
    app.UseFundoMiddlewares(app.Environment);

    app.UseSwaggerDocumentation();

    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/index.html");
            return;
        }

        await next();
    });

    // Migrations
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<LoanDbContext>();
        db.Database.Migrate();
    }

    app.MapControllers();
    app.Run();

    Log.Information("Fundo.API started successfully");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Fundo.API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }