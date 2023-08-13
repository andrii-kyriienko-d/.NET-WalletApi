using WalletApi.Core.Models.WalletAccount;

namespace WalletApi.Core.Services.Interfaces;

public interface IWalletAccountService
{
    Task<Guid> CreateAccountAsync(WalletAccountUpsertModel model, CancellationToken
        cancellationToken = default);

    Task UpdateAccountAsync(Guid id, WalletAccountUpsertModel model, CancellationToken
        cancellationToken = default);

    Task DeleteAccountAsync(Guid id, CancellationToken
        cancellationToken = default);

    Task<WalletAccountViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<WalletAccountViewModel>> GetAllAsync(CancellationToken cancellationToken = default);
}