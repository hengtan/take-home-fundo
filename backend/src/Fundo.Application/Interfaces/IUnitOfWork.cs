namespace Fundo.Application.Interfaces;

public interface IUnitOfWork
{
    ILoanRepository LoanRepository { get; }
    IHistoryRepository HistoryRepository { get; }

    Task<int> CompleteAsync(CancellationToken cancellationToken);
}