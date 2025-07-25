using Fundo.Application.Interfaces;
using Fundo.Domain.Entities;
using MediatR;

namespace Fundo.Application.Commands.Loans.Create;

public class CreateLoanCommandHandler(ILoanRepository loanRepository) : IRequestHandler<CreateLoanCommand, Guid>
{
    public async Task<Guid> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = new Loan(
            amount: request.Amount,
            currentBalance: request.CurrentBalance,
            applicantName: request.ApplicantName
        );

        await loanRepository.AddAsync(loan, cancellationToken);

        return loan.Id;
    }
}