using Fundo.Domain.Entities;

namespace Fundo.Application.Interfaces;

public interface IHistoryRepository
{
    Task AddAsync(History history, CancellationToken cancellationToken);
    Task <IReadOnlyList<History>> GetByLoanIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<History>> GetAllAsync(CancellationToken cancellationToken);
}