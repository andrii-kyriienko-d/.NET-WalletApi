using WalletApi.Data.Entities.Abstractions;

namespace WalletApi.Core.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseDbEntity
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    TEntity Update(TEntity entity);
    Task DeleteAsync(Guid id);
    Task<TEntity?> GetAsync(Guid id);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task SubmitAsync(CancellationToken cancellationToken = default);
}