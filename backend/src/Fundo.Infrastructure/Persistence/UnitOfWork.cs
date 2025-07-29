using Fundo.Application.Interfaces;

namespace Fundo.Infrastructure.Persistence;

public class UnitOfWork(LoanDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}