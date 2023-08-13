using Microsoft.EntityFrameworkCore;
using WalletApi.Core.Models;
using WalletApi.Core.Repositories.Interfaces;
using WalletApi.Data.DatabaseContexts;
using WalletApi.Data.Entities;

namespace WalletApi.Core.Repositories;

internal sealed class WalletTransactionRepository : BaseRepository<WalletTransaction>, IWalletTransactionRepository
{
    private readonly WalletDbContext _walletDbContext;

    public WalletTransactionRepository(WalletDbContext walletDbContext) : base(walletDbContext)
    {
        _walletDbContext = walletDbContext;
    }

    public async Task<List<WalletTransaction>> GetByUserIdAsync(Guid userId,
        PagingModel pagingModel = null,
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            return new(0);
        }

        var transactions =
            await _walletDbContext.WalletTransactions
                .Where(item => item.User.Id.Equals(userId))
                .Take(pagingModel?.Take ?? 10)
                .ToListAsync(cancellationToken: cancellationToken);

        return transactions;
    }

    public async Task<List<WalletTransaction>> GetByAccountIdAsync(Guid accountId,
        PagingModel pagingModel = null,
        CancellationToken cancellationToken = default)
    {
        if (accountId == Guid.Empty)
        {
            return new(0);
        }

        var transactions =
            await _walletDbContext.WalletTransactions
                .Where(item => item.Account.Id.Equals(accountId))
                .Take(pagingModel?.Take ?? 10)
                .ToListAsync(cancellationToken: cancellationToken);

        return transactions;
    }
}