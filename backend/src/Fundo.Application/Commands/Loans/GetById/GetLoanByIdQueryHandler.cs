using Fundo.Application.DTOs;
using Fundo.Application.Interfaces;
using MediatR;

namespace Fundo.Application.Commands.Loans.GetById;

public class GetLoanByIdQueryHandler(ILoanRepository loanRepository)
    : IRequestHandler<GetLoanByIdQuery, LoanDetailsDto?>
{
    public async Task<LoanDetailsDto?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await loanRepository.GetByIdAsync(request.Id, cancellationToken);

        if (loan is null) return null;

        return new LoanDetailsDto
        {
            Id = loan.Id,
            Amount = loan.Amount,
            CurrentBalance = loan.CurrentBalance,
            ApplicantName = loan.ApplicantName,
            Status = loan.Status.ToString()
        };
    }
}