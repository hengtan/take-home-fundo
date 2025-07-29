using MediatR;

namespace Fundo.Application.Commands.Loans.Payment;


public class AddPaymentCommand : IRequest
{
    public Guid LoanId { get; init; }
    public decimal Amount { get; init; }
}