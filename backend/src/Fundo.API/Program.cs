using Fundo.API.Extensions;
using Fundo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<Fundo.Application.Commands.Loans.Create.CreateLoanCommand>());

// App
var app = builder.Build();
app.UseFundoMiddlewares(app.Environment);
app.MapControllers();
app.Run();