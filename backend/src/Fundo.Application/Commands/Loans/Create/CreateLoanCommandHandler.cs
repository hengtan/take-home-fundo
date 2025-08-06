using Fundo.Application.Common.Errors;
using Fundo.Application.Common.Results;
using Fundo.Application.Errors.ErrorsMessages;
using Fundo.Application.Interfaces;
using Fundo.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundo.Application.Commands.Loans.Create;

public class CreateLoanCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<CreateLoanCommandHandler> logger
) : IRequestHandler<CreateLoanCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting loan creation for applicant: {Applicant}", request.ApplicantName);

        if (IsInvalid(request))
        {
            logger.LogWarning("Invalid loan request for applicant: {Applicant}", request.ApplicantName);
            return Result<Guid>.Failure(Error.Validation(ErrorMessages.LoanAmountAndBalanceMustBePositive));
        }

        if (unitOfWork.LoanRepository is null)
        {
            logger.LogError("LoanRepository is null in UnitOfWork");
            return Result<Guid>.Failure(Error.Internal("Loan repository not configured."));
        }
        if (unitOfWork.HistoryRepository is null)
        {
            logger.LogError("HistoryRepository is null in UnitOfWork");
            return Result<Guid>.Failure(Error.Internal("History repository not configured."));
        }

        var loan = Loan.Create(Guid.NewGuid(), request.Amount, request.CurrentBalance, request.ApplicantName);

        await unitOfWork.LoanRepository.AddAsync(loan, cancellationToken);

        var history = new History(
            loan.Id,
            description: $"Loan Created with Amount: {loan.Amount} and Current Balance: {loan.CurrentBalance} on date: {DateTime.UtcNow}",
            created: DateTime.UtcNow
        );

        await unitOfWork.HistoryRepository.AddAsync(history, cancellationToken);

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);

            logger.LogInformation("Loan created successfully for applicant: {Applicant}, LoanId: {LoanId}",
                loan.ApplicantName, loan.Id);
            return Result<Guid>.Success(loan.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to persist loan for applicant: {Applicant}", loan.ApplicantName);
            return Result<Guid>.Failure(Error.Internal(ErrorMessages.LoanSaveInternalError));
        }
    }

    private static bool IsInvalid(CreateLoanCommand request)
    {
        return request.Amount <= 0 || request.CurrentBalance < 0;
    }
}