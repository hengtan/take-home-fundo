using MediatR;

namespace Fundo.Application.Commands.Loans.RegisterPayment;

public record RegisterPaymentCommand(Guid LoanId, decimal Amount) : IRequest;