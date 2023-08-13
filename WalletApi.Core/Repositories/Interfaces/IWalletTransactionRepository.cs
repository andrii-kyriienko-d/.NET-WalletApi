using WalletApi.Core.Models;
using WalletApi.Data.Entities;

namespace WalletApi.Core.Repositories.Interfaces;

public interface IWalletTransactionRepository : IBaseRepository<WalletTransaction>
{
    Task<List<WalletTransaction>> GetByUserIdAsync(Guid userId,
        PagingModel pagingModel = null,
        CancellationToken cancellationToken = default);

    Task<List<WalletTransaction>> GetByAccountIdAsync(Guid accountId,
        PagingModel pagingModel = null,
        CancellationToken cancellationToken = default);
}