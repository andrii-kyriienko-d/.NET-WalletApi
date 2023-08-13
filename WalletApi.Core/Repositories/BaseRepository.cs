using Microsoft.EntityFrameworkCore;
using WalletApi.Core.Repositories.Interfaces;
using WalletApi.Data.DatabaseContexts;
using WalletApi.Data.Entities.Abstractions;

namespace WalletApi.Core.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseDbEntity
{
    private readonly WalletDbContext _walletDbContext;

    public BaseRepository(WalletDbContext walletDbContext)
    {
        _walletDbContext = walletDbContext;
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new ArgumentNullException();
        }

        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        
        entity.CreatedDate = DateTime.UtcNow;
        
        await _walletDbContext.AddAsync(entity, cancellationToken);
    }

    public TEntity Update(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException();
        }
        
        entity.ModifiedDate = DateTime.UtcNow;

        return _walletDbContext.Update(entity).Entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _walletDbContext.FindAsync<TEntity>(id);
        if (entity is null)
        {
            return;
        }

        _walletDbContext.Remove(entity);
    }

    public async Task<TEntity?> GetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        var entity = await _walletDbContext.FindAsync<TEntity>(id);

        return entity;
    }
    
    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = _walletDbContext.Set<TEntity>();

        return await entities.ToListAsync(cancellationToken);
    }

    public async Task SubmitAsync(CancellationToken cancellationToken = default)
    {
        await _walletDbContext.SaveChangesAsync(cancellationToken);
    }
}