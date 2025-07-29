using Fundo.Application.Interfaces;
using MediatR;

namespace Fundo.Application.Commands.Loans.Payment;

public class AddPaymentCommandHandler(ILoanRepository loanRepository) : IRequestHandler<AddPaymentCommand>
{
    public async Task Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        var loan = await loanRepository.GetByIdAsync(request.LoanId, cancellationToken)
                   ?? throw new InvalidOperationException("Empréstimo não encontrado.");

        loan.RegisterPayment(request.Amount);

        await loanRepository.SaveChangesAsync(cancellationToken);
    }
}