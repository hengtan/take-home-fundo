using Fundo.Application.Common.Errors;
using Fundo.Application.Common.Results;
using Fundo.Application.Errors.ErrorsMessages;
using Fundo.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Unit = Fundo.Application.Common.Results.Unit;

namespace Fundo.Application.Commands.Loans.RegisterPayment;

public class RegisterPaymentCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<RegisterPaymentCommandHandler> logger)
    : IRequestHandler<RegisterPaymentCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RegisterPaymentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to register payment for LoanId: {LoanId}", request.LoanId);

        var loan = await GetLoanAsync(request.LoanId, cancellationToken);
        if (loan is null)
        {
            logger.LogWarning("Loan not found: {LoanId}", request.LoanId);
            return Result<Unit>.Failure(Error.NotFound("Loan not found."));
        }

        try
        {
            RegisterPayment(loan, request.Amount);
            await unitOfWork.CompleteAsync(cancellationToken);

            logger.LogInformation("Payment of {Amount} registered successfully for LoanId: {LoanId}",
                request.Amount, request.LoanId);

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while registering payment for LoanId: {LoanId}", request.LoanId);
            return Result<Unit>.Failure(Error.Internal(ErrorMessages.UnexpectedErrorOnRegistration));
        }
    }

    private async Task<Domain.Entities.Loan?> GetLoanAsync(Guid loanId, CancellationToken cancellationToken)
    {
        return await unitOfWork.LoanRepository.GetByIdAsync(loanId, cancellationToken);
    }

    private static void RegisterPayment(Domain.Entities.Loan loan, decimal amount)
    {
        loan.RegisterPayment(amount);
    }
}