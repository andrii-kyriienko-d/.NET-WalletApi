using System.Text;
using AutoMapper;
using WalletApi.Core.Models.WalletTransaction;
using WalletApi.Core.Repositories.Interfaces;
using WalletApi.Core.Services.Interfaces;
using WalletApi.Data.Entities;
using WalletApi.Data.Enums;

namespace WalletApi.Core.Services;

internal sealed class WalletTransactionService : IWalletTransactionService
{
    private readonly IWalletTransactionRepository _walletTransactionRepository;
    private readonly IWalletAccountRepository _walletAccountRepository;
    private readonly IWalletUserRepository _walletUserRepository;
    private readonly IMapper _mapper;

    public WalletTransactionService(IWalletTransactionRepository walletTransactionRepository, IMapper mapper,
        IWalletUserRepository walletUserRepository, IWalletAccountRepository walletAccountRepository)
    {
        _walletTransactionRepository = walletTransactionRepository;
        _mapper = mapper;
        _walletUserRepository = walletUserRepository;
        _walletAccountRepository = walletAccountRepository;
    }

    public async Task<Guid> CreateTransactionAsync(WalletTransactionUpsertModel model, CancellationToken
        cancellationToken = default)
    {
        var transaction = _mapper.Map<WalletTransaction>(model);
        transaction.Account = await _walletAccountRepository.GetAsync(model.AccountId);
        transaction.User = await _walletUserRepository.GetAsync(model.UserId);

        await _walletTransactionRepository.CreateAsync(transaction, cancellationToken);
        await _walletTransactionRepository.SubmitAsync(cancellationToken);

        return transaction.Id;
    }

    public async Task UpdateTransactionAsync(Guid id, WalletTransactionUpsertModel model, CancellationToken
        cancellationToken = default)
    {
        var transaction = _mapper.Map<WalletTransaction>(model);
        transaction.Id = id;

        _walletTransactionRepository.Update(transaction);
        await _walletTransactionRepository.SubmitAsync(cancellationToken);
    }

    public async Task DeleteTransactionAsync(Guid id, CancellationToken
        cancellationToken = default)
    {
        await _walletTransactionRepository.DeleteAsync(id);
        await _walletTransactionRepository.SubmitAsync(cancellationToken);
    }

    public async Task<WalletTransactionViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var transaction = await _walletTransactionRepository.GetAsync(id);
        return transaction is null ? null : await GetTransactionViewModelAsync(transaction);
    }

    public async Task<List<WalletTransactionViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var transactions = await _walletTransactionRepository.GetAllAsync(cancellationToken);
        var viewModels = new List<WalletTransactionViewModel>(transactions.Count);

        foreach (var transaction in transactions)
        {
            viewModels.Add(await GetTransactionViewModelAsync(transaction));
        }

        return viewModels;
    }

    public async Task<List<WalletTransactionViewModel>> GetByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var transactions =
            await _walletTransactionRepository.GetByUserIdAsync(userId, cancellationToken: cancellationToken);
        var viewModels = new List<WalletTransactionViewModel>(transactions.Count);

        foreach (var transaction in transactions)
        {
            viewModels.Add(await GetTransactionViewModelAsync(transaction));
        }

        return viewModels;
    }

    public async Task<List<WalletTransactionViewModel>> GetByAccountIdAsync(Guid accountId,
        CancellationToken cancellationToken = default)
    {
        var transactions =
            await _walletTransactionRepository.GetByAccountIdAsync(accountId, cancellationToken: cancellationToken);
        var viewModels = new List<WalletTransactionViewModel>(transactions.Count);

        foreach (var transaction in transactions)
        {
            viewModels.Add(await GetTransactionViewModelAsync(transaction));
        }

        return viewModels;
    }

    private async Task<WalletTransactionViewModel> GetTransactionViewModelAsync(WalletTransaction transaction)
    {
        var viewModel = _mapper.Map<WalletTransactionViewModel>(transaction);

        viewModel.UserName = (await _walletUserRepository.GetAsync(transaction.UserId))?.Name ?? string.Empty;
        viewModel.Icon = "https://cdn-icons-png.flaticon.com/512/1581/1581942.png";
        viewModel.Description = transaction.IsPending ? $"Pending {transaction.Description}" : transaction.Description;
        var sumString = new StringBuilder();
        if (transaction.Type == TransactionType.Payment)
        {
            sumString.Append('+');
        }

        sumString.Append(transaction.Sum);

        viewModel.Sum = sumString.ToString();

        return viewModel;
    }
}