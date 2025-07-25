using MediatR;

namespace Fundo.Application.Commands.Loans.Create;

public record CreateLoanCommand(
    decimal Amount,
    decimal CurrentBalance,
    string ApplicantName
) : IRequest<Guid>;