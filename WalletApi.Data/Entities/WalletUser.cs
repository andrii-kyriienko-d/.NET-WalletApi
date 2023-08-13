using WalletApi.Data.Entities.Abstractions;

namespace WalletApi.Data.Entities;

public sealed class WalletUser : BaseDbEntity
{
    public string Name { get; set; }
    public ICollection<WalletAccount> Accounts { get; set; }
    public ICollection<WalletTransaction> Transactions { get; set; }
}