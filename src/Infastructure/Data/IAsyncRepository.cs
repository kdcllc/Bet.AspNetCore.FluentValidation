using Bet.BuildingBlocks.Domain.Abstractions.Specifications.Interfaces;

using Domain;

namespace Infastructure.Data;

public interface IAsyncRepository<T> where T : DomainEntity
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<T>> ListAllAsync(int perPage, int page, CancellationToken cancellationToken);

    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    Task DeleteAsync(T entity, CancellationToken cancellationToken);

    Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken);
}
