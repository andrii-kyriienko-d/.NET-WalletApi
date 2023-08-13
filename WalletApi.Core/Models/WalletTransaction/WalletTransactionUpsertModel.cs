using WalletApi.Data.Enums;

namespace WalletApi.Core.Models.WalletTransaction;

public sealed class WalletTransactionUpsertModel
{
    public TransactionType Type { get; set; }
    public double Sum { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public bool IsPending { get; set; }
    public Guid AccountId { get; set; }
    public Guid UserId { get; set; }
}