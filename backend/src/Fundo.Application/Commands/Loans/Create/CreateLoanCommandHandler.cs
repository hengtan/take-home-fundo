using Fundo.Application.Common.Errors;
using Fundo.Application.Common.Results;
using Fundo.Application.Interfaces;
using Fundo.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundo.Application.Commands.Loans.Create;

public class CreateLoanCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateLoanCommandHandler> logger)
    : IRequestHandler<CreateLoanCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount <= 0 || request.CurrentBalance < 0)
            return Result<Guid>.Failure(Error.Validation("Loan amount and balance must be greater than zero."));

        try
        {
            var loan = new Loan(request.Amount, request.CurrentBalance, request.ApplicantName);

            await unitOfWork.LoanRepository.AddAsync(loan, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Guid>.Success(loan.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating loan.");
            return Result<Guid>.Failure(Error.Internal("Internal server error."));
        }
    }
}