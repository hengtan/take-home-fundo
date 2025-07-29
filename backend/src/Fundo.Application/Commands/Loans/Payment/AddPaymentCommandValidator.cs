using FluentValidation;

namespace Fundo.Application.Commands.Loans.Payment;

public class AddPaymentCommandValidator : AbstractValidator<AddPaymentCommand>
{
    public AddPaymentCommandValidator()
    {
        RuleFor(x => x.LoanId)
            .NotEmpty().WithMessage("LoanId é obrigatório.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("O valor do pagamento deve ser maior que zero.");
    }
}