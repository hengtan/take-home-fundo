using Fundo.Application.Interfaces;
using MediatR;

namespace Fundo.Application.Commands.Loans.RegisterPayment;

public class RegisterPaymentCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterPaymentCommand>
{
    public async Task Handle(RegisterPaymentCommand request, CancellationToken cancellationToken)
    {
        var loan = await unitOfWork.LoanRepository.GetByIdAsync(request.LoanId, cancellationToken)
                   ?? throw new Exception("Loan not found.");

        loan.RegisterPayment(request.Amount);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}