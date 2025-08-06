using Fundo.Application.Interfaces;

namespace Fundo.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context, ILoanRepository loanRepository, IHistoryRepository historyRepository) : IUnitOfWork
{
    public ILoanRepository LoanRepository => loanRepository;
    public IHistoryRepository HistoryRepository => historyRepository;

    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}