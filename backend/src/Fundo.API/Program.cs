using Fundo.API.Extensions;
using Fundo.Infrastructure.DependencyInjection;
using Fundo.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

var config = builder.Configuration;
builder.Services.Configure<JwtSettings>(config.GetSection("Jwt"));
builder.Services.AddInfrastructure(config);
builder.Services.AddApplicationServices();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
builder.Services.AddAuthenticationExtension(config);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseExceptionHandling();
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseSwaggerExtension(env);

app.Run();