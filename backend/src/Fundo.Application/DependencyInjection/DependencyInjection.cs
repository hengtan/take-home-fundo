using FluentValidation;
using Fundo.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using Fundo.Application.Commands.Loans.Create;
using Fundo.Application.Validators.Loans;
using MediatR;

namespace Fundo.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<CreateLoanCommand>());

        services.AddValidatorsFromAssemblyContaining<CreateLoanCommandValidator>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}