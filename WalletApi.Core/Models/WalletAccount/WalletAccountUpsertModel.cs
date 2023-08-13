namespace WalletApi.Core.Models.WalletAccount;

public sealed class WalletAccountUpsertModel
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}