namespace WalletApi.Core.Models.WalletTransaction;

public sealed class WalletTransactionViewModel
{
    public string Sum { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string UserName { get; set; }
    public string Icon { get; set; } //icon file link
}