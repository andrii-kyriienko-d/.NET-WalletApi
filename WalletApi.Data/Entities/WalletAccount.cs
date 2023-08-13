using WalletApi.Data.Entities.Abstractions;

namespace WalletApi.Data.Entities;

public sealed class WalletAccount : BaseDbEntity
{
    public string Name { get; set; }
    public double Balance { get; set; } 
    public string DailyPoints { get; set; }
    public Guid OwnerId { get; set; }
    public WalletUser Owner { get; set; }
    public ICollection<WalletTransaction> Transactions { get; set; }
}