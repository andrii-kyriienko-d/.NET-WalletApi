using AutoMapper;
using WalletApi.Core.Models.WalletTransaction;
using WalletApi.Data.Entities;

namespace WalletApi.Core.Automapper;

public sealed class WalletTransactionMapping : Profile
{
    public WalletTransactionMapping()
    {
        CreateMap<WalletTransactionUpsertModel, WalletTransaction>()
            .ForMember(walletTransaction => walletTransaction.User,
                options => options.Ignore())
            .ForMember(walletTransaction => walletTransaction.Account,
                options => options.Ignore());

        CreateMap<WalletTransaction, WalletTransactionViewModel>()
            .ForMember(walletTransaction => walletTransaction.UserName,
                options => options.Ignore())
            .ForMember(walletTransaction => walletTransaction.Sum,
                options => options.Ignore())
            .ForMember(walletTransactionViewModel => walletTransactionViewModel.Date,
                walletTransaction =>
                    walletTransaction.MapFrom(transaction => transaction.CreatedDate));
    }
}