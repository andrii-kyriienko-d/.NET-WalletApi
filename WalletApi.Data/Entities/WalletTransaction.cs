using WalletApi.Data.Entities.Abstractions;
using WalletApi.Data.Enums;

namespace WalletApi.Data.Entities;

public sealed class WalletTransaction : BaseDbEntity
{
    public TransactionType Type { get; set; }
    public double Sum { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsPending { get; set; }
    public Guid AccountId { get; set; }
    public WalletAccount Account { get; set; }
    public Guid UserId { get; set; }
    public WalletUser User { get; set; }
}