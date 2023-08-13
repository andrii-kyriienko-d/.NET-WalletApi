using System.Numerics;
using AutoMapper;
using WalletApi.Core.Models.WalletAccount;
using WalletApi.Core.Repositories.Interfaces;
using WalletApi.Core.Services.Interfaces;
using WalletApi.Data.Entities;
using WalletApi.Data.Enums;

namespace WalletApi.Core.Services;

internal sealed class WalletAccountService : IWalletAccountService
{
    private readonly IWalletAccountRepository _walletAccountRepository;
    private readonly IWalletTransactionRepository _walletTransactionRepository;
    private readonly IWalletUserRepository _walletUserRepository;
    private readonly IMapper _mapper;

    public WalletAccountService(IWalletAccountRepository walletAccountRepository, IMapper mapper,
        IWalletTransactionRepository walletTransactionRepository, IWalletUserRepository walletUserRepository)
    {
        _walletAccountRepository = walletAccountRepository;
        _mapper = mapper;
        _walletTransactionRepository = walletTransactionRepository;
        _walletUserRepository = walletUserRepository;
    }

    public async Task<Guid> CreateAccountAsync(WalletAccountUpsertModel model,
        CancellationToken cancellationToken = default)
    {
        var account = _mapper.Map<WalletAccount>(model);
        account.DailyPoints = CalculatePoints();

        account.Owner = await _walletUserRepository.GetAsync(model.OwnerId);

        await _walletAccountRepository.CreateAsync(account, cancellationToken: cancellationToken);
        await _walletAccountRepository.SubmitAsync(cancellationToken);

        return account.Id;
    }

    public async Task UpdateAccountAsync(Guid id, WalletAccountUpsertModel model,
        CancellationToken cancellationToken = default)
    {
        var existAccount = await _walletAccountRepository.GetAsync(id);

        var account = _mapper.Map(model, existAccount) ?? _mapper.Map<WalletAccount>(model);

        account.Id = id;
        account.DailyPoints = CalculatePoints();

        var transactions =
            await _walletTransactionRepository.GetByAccountIdAsync(account.Id, cancellationToken: cancellationToken);

        account.Balance =
            transactions.Where(item => item.Type == TransactionType.Payment).Select(item => item.Sum)
                .Sum();
        account.Balance -= transactions.Where(item => item.Type == TransactionType.Credit)
            .Select(item => item.Sum)
            .Sum();

        account.Owner = await _walletUserRepository.GetAsync(model.OwnerId) ?? account.Owner;

        _walletAccountRepository.Update(account);
        await _walletAccountRepository.SubmitAsync(cancellationToken);
    }

    public async Task DeleteAccountAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _walletAccountRepository.DeleteAsync(id);
        await _walletAccountRepository.SubmitAsync(cancellationToken);
    }

    public async Task<WalletAccountViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await _walletAccountRepository.GetAsync(id);
        return account is null ? null : await GetAccountViewModelAsync(account, cancellationToken);
    }

    public async Task<List<WalletAccountViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var accounts = await _walletAccountRepository.GetAllAsync(cancellationToken);
        var viewModels = new List<WalletAccountViewModel>(accounts.Count);

        foreach (var account in accounts)
        {
            viewModels.Add(await GetAccountViewModelAsync(account, cancellationToken));
        }

        return viewModels;
    }

    private async Task<WalletAccountViewModel> GetAccountViewModelAsync(WalletAccount account,
        CancellationToken cancellationToken = default)
    {
        var viewModel = _mapper.Map<WalletAccountViewModel>(account);
        var transactions =
            await _walletTransactionRepository.GetByAccountIdAsync(account.Id, cancellationToken: cancellationToken);

        viewModel.Balance =
            transactions.Where(item => item.Type == TransactionType.Payment).Select(item => item.Sum)
                .Sum();
        viewModel.Balance -= transactions.Where(item => item.Type == TransactionType.Credit)
            .Select(item => item.Sum)
            .Sum();

        viewModel.DailyPoints = CalculatePoints();

        return viewModel;
    }

    private static string CalculatePoints()
    {
        var seasonDay = 0;
        var date = DateTime.UtcNow;

        seasonDay = date.Month switch
        {
            //winter
            12 => DateTime.UtcNow.Day,
            1 => DateTime.DaysInMonth(DateTime.UtcNow.Year - 1, 12) + DateTime.UtcNow.Day,
            2 => DateTime.DaysInMonth(DateTime.UtcNow.Year - 1, 12) + DateTime.DaysInMonth(DateTime.UtcNow.Year, 1) +
                 DateTime.UtcNow.Day,
            //spring
            3 => DateTime.UtcNow.Day,
            4 => DateTime.DaysInMonth(DateTime.UtcNow.Year, 3) + DateTime.UtcNow.Day,
            5 => DateTime.DaysInMonth(DateTime.UtcNow.Year, 4) + DateTime.DaysInMonth(DateTime.UtcNow.Year, 5) +
                 DateTime.UtcNow.Day,
            //summer
            6 => DateTime.UtcNow.Day,
            7 => DateTime.DaysInMonth(DateTime.UtcNow.Year, 6) + DateTime.UtcNow.Day,
            8 => DateTime.DaysInMonth(DateTime.UtcNow.Year, 6) + DateTime.DaysInMonth(DateTime.UtcNow.Year, 7) +
                 DateTime.UtcNow.Day,
            //autumn
            9 => DateTime.UtcNow.Day,
            10 => DateTime.DaysInMonth(DateTime.UtcNow.Year, 10) + DateTime.UtcNow.Day,
            11 => DateTime.DaysInMonth(DateTime.UtcNow.Year, 10) + DateTime.DaysInMonth(DateTime.UtcNow.Year, 11) +
                  DateTime.UtcNow.Day,
            _ => seasonDay
        };

        BigInteger prevPoints = 0;
        BigInteger monthPoints = 2;

        BigInteger calculatedPoints = 0;

        for (var day = 0; day <= seasonDay; day++)
        {
            calculatedPoints = monthPoints;
            var prevPrevPoints = prevPoints;
            prevPoints = monthPoints;
            monthPoints += prevPoints / 10 * 6;
            monthPoints += prevPrevPoints;
        }

        return calculatedPoints > 1000000000000000000 ? $"{calculatedPoints / 1000000000000000}Sx" :
            calculatedPoints > 1000000000000000 ? $"{calculatedPoints / 1000000000000000}Qt" :
            calculatedPoints > 1000000000000000 ? $"{calculatedPoints / 1000000000000000}Q" :
            calculatedPoints > 1000000000000 ? $"{calculatedPoints / 1000000000000}T" :
            calculatedPoints > 1000000000 ? $"{calculatedPoints / 1000000000}B" :
            calculatedPoints > 1000000 ? $"{calculatedPoints / 1000000}M" :
            calculatedPoints > 100000 ? $"{calculatedPoints / 1000000}K" :
            calculatedPoints > 1000 ? $"{calculatedPoints / 1000}k" : calculatedPoints.ToString();
    }
}