using WalletApi.Core.Repositories.Interfaces;
using WalletApi.Data.DatabaseContexts;
using WalletApi.Data.Entities;

namespace WalletApi.Core.Repositories;

internal sealed class WalletAccountRepository : BaseRepository<WalletAccount>, IWalletAccountRepository
{
    private readonly WalletDbContext _walletDbContext;

    public WalletAccountRepository(WalletDbContext walletDbContext) : base(walletDbContext)
    {
        _walletDbContext = walletDbContext;
    }
}