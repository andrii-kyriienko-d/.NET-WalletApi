using AutoMapper;
using WalletApi.Core.Models.WalletAccount;
using WalletApi.Data.Entities;

namespace WalletApi.Core.Automapper;

public sealed class WalletAccountMapping : Profile
{
    public WalletAccountMapping()
    {
        CreateMap<WalletAccountUpsertModel, WalletAccount>()
            .ForMember(walletTransaction => walletTransaction.Transactions,
                options => options.Ignore())
            .ForMember(walletTransaction => walletTransaction.Balance,
                options => options.Ignore())
            .ForMember(walletTransaction => walletTransaction.DailyPoints,
                options => options.Ignore())
            .ForMember(walletTransaction => walletTransaction.Owner,
                options => options.Ignore());
        
        CreateMap<WalletAccount, WalletAccountViewModel>()
            .ForMember(walletTransaction => walletTransaction.Balance,
                options => options.Ignore())
            .ForMember(walletTransaction => walletTransaction.DailyPoints,
                options => options.Ignore());
    }
}