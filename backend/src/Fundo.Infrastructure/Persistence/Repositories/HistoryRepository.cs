using Fundo.Application.Interfaces;
using Fundo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Infrastructure.Persistence.Repositories;

public class HistoryRepository(HistoryDbContext context) : IHistoryRepository
{
    public async Task AddAsync(History history, CancellationToken cancellationToken)
    {
        await context.History.AddAsync(history, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<History>> GetByLoanIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.History
            .AsNoTracking()
            .Where(h => h.LoandId == id)
            .OrderBy(h => h.Created)
            .ToListAsync(cancellationToken);;
    }

    public async Task<List<History>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.History
            .AsNoTracking()
            .OrderBy(l => l.LoandId)
            .ToListAsync(cancellationToken);
    }
}