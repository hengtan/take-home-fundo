using Fundo.Application.Common.Errors;
using Fundo.Application.Common.Results;
using Fundo.Application.Interfaces;
using Fundo.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Unit = Fundo.Application.Common.Results.Unit;

namespace Fundo.Application.Commands.Loans.Payment;

// public class AddPaymentCommandHandler(
//     ILoanRepository loanRepository,
//     IUnitOfWork unitOfWork,
//     IHistoryRepository historyRepository,
//     ILogger<AddPaymentCommandHandler> logger)
//     : IRequestHandler<AddPaymentCommand, Result<Unit>>
// {
//     public async Task<Result<Unit>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
//     {
//         LogStart(request);
//
//         var loan = await RetrieveLoanAsync(request.LoanId, cancellationToken);
//         if (loan is null)
//         {
//             return LogAndReturnLoanNotFound(request);
//         }
//
//         return await ProcessPaymentAsync(loan, request.Amount, request.LoanId, cancellationToken);
//     }
//
//     private void LogStart(AddPaymentCommand request)
//     {
//         logger.LogInformation("Processing payment for LoanId: {LoanId}", request.LoanId);
//     }
//
//     private async Task<Loan?> RetrieveLoanAsync(Guid loanId, CancellationToken cancellationToken)
//     {
//         return await loanRepository.GetByIdAsync(loanId, cancellationToken);
//     }
//
//     private Result<Unit> LogAndReturnLoanNotFound(AddPaymentCommand request)
//     {
//         logger.LogWarning("Loan not found: {LoanId}", request.LoanId);
//         return Result<Unit>.Failure(Error.NotFound("Loan not found."));
//     }
//
//     private async Task<Result<Unit>> ProcessPaymentAsync(Loan loan, decimal amount, Guid loanId, CancellationToken cancellationToken)
//     {
//         try
//         {
//             loan.RegisterPayment(amount);
//             await unitOfWork.CompleteAsync(cancellationToken);
//
//             logger.LogInformation(
//                 "Payment of {Amount} registered successfully for LoanId: {LoanId}", amount, loanId
//             );
//
//             var history = new History(
//                 loan.Id,
//                 description:"Payment of " + amount + " registered on "+ DateTime.UtcNow,
//                 created: DateTime.Now
//             );
//
//             await historyRepository.AddAsync(history, cancellationToken);
//
//             return Result<Unit>.Success(Unit.Value);
//         }
//         catch (Exception ex)
//         {
//             logger.LogError(ex, "Failed to register payment for LoanId: {LoanId}", loanId);
//             return Result<Unit>.Failure(Error.Internal("An unexpected error occurred while processing the payment."));
//         }
//     }
// }

public class AddPaymentCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<AddPaymentCommandHandler> logger)
    : IRequestHandler<AddPaymentCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing payment for LoanId: {LoanId}", request.LoanId);

        var loan = await unitOfWork.LoanRepository.GetByIdAsync(request.LoanId, cancellationToken);
        if (loan is null)
        {
            logger.LogWarning("Loan not found: {LoanId}", request.LoanId);
            return Result<Unit>.Failure(Error.NotFound("Loan not found."));
        }

        try
        {
            loan.RegisterPayment(request.Amount);

            var history = new History(
                loan.Id,
                description: $"Payment of {request.Amount} registered on {DateTime.UtcNow}",
                created: DateTime.UtcNow
            );

            await unitOfWork.HistoryRepository.AddAsync(history, cancellationToken);

            await unitOfWork.CompleteAsync(cancellationToken);

            logger.LogInformation(
                "Payment of {Amount} registered successfully for LoanId: {LoanId}", request.Amount, request.LoanId
            );

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to register payment for LoanId: {LoanId}", request.LoanId);
            return Result<Unit>.Failure(Error.Internal("An unexpected error occurred while processing the payment."));
        }
    }
}