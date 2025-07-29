namespace Fundo.Application.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}