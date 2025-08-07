using Fundo.Application.Common.Errors;
using Fundo.Application.Common.Results;
using Fundo.Application.Interfaces;
using Fundo.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Unit = Fundo.Application.Common.Results.Unit;

namespace Fundo.Application.Commands.Loans.Payment;
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