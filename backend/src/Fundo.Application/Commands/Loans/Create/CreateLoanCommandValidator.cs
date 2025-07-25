using FluentValidation;

namespace Fundo.Application.Commands.Loans.Create;

public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Loan amount must be greater than zero.");

        RuleFor(x => x.CurrentBalance)
            .GreaterThanOrEqualTo(0).WithMessage("Current balance must be zero or more.")
            .LessThanOrEqualTo(x => x.Amount).WithMessage("Current balance cannot exceed amount.");

        RuleFor(x => x.ApplicantName)
            .NotEmpty().WithMessage("Applicant name is required.")
            .MaximumLength(100);
    }
}