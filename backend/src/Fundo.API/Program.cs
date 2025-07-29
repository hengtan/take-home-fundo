using Fundo.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<Fundo.Application.Commands.Loans.Create.CreateLoanCommand>());

builder.Services.AddAutoMapper(typeof(Fundo.Application.DTOs.LoanListItemDto).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fundo Loan API",
        Version = "v1",
        Description = "API for managing loan applications"
    });
});

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fundo Loan API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();