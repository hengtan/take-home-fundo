using FluentValidation;
using Fundo.Application.Behaviors;
using Fundo.Application.Commands.Loans.Create;
using Fundo.Application.Commands.Loans.Payment;
using Fundo.Application.Commands.Loans.RegisterPayment;
using Fundo.Application.Queries.Loan.GetById;
using Fundo.Application.Queries.Loan.List;
using Fundo.Application.Validators.Loans;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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