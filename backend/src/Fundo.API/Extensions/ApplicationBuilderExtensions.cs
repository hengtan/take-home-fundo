using Fundo.API.Middlewares;

namespace Fundo.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseFundoMiddlewares(this IApplicationBuilder app, IHostEnvironment env)
    {
        if (!env.IsProduction())
        {
            app.UseSwaggerDocumentation();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}