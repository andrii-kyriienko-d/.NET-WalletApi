using WalletApi.Core.Repositories.Interfaces;
using WalletApi.Data.DatabaseContexts;
using WalletApi.Data.Entities;

namespace WalletApi.Core.Repositories;

internal sealed class WalletUserRepository : BaseRepository<WalletUser>, IWalletUserRepository
{
    public WalletUserRepository(WalletDbContext walletDbContext) : base(walletDbContext)
    {
    }
}