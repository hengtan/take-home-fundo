using Fundo.API.Extensions;
using Fundo.Application.DependencyInjection;
using Fundo.Infrastructure.DependencyInjection;
using Fundo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5000");

// Services
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<Fundo.Application.Commands.Loans.Create.CreateLoanCommand>());

// App
var app = builder.Build();

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LoanDbContext>();
    db.Database.Migrate();
}

app.MapControllers();
app.Run();

public partial class Program { }