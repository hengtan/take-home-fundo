using Fundo.Application.DTOs;
using Fundo.Application.Interfaces;
using MediatR;

namespace Fundo.Application.Queries.Loan.List;


public class GetAllLoansQueryHandler(ILoanRepository loanRepository)
    : IRequestHandler<GetAllLoansQuery, List<LoanListItemDto>>
{
    public async Task<List<LoanListItemDto>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
    {
        var loans = await loanRepository.GetAllAsync(cancellationToken);

        return loans.Select(loan => new LoanListItemDto
        {
            Id = loan.Id,
            ApplicantName = loan.ApplicantName,
            CurrentBalance = loan.CurrentBalance,
            Status = loan.Status.ToString()
        }).ToList();
    }
}