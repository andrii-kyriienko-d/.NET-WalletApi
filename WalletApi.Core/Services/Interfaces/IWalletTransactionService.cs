using WalletApi.Core.Models.WalletTransaction;

namespace WalletApi.Core.Services.Interfaces;

public interface IWalletTransactionService
{
    Task<Guid> CreateTransactionAsync(WalletTransactionUpsertModel model, CancellationToken
        cancellationToken = default);

    Task UpdateTransactionAsync(Guid id, WalletTransactionUpsertModel model, CancellationToken
        cancellationToken = default);
    
    Task DeleteTransactionAsync(Guid id, CancellationToken
        cancellationToken = default);

    Task<WalletTransactionViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<WalletTransactionViewModel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<WalletTransactionViewModel>> GetByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default);

    Task<List<WalletTransactionViewModel>> GetByAccountIdAsync(Guid accountId,
        CancellationToken cancellationToken = default);
}