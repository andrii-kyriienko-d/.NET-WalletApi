using AutoMapper;
using WalletApi.Core.Models.WalletUser;
using WalletApi.Data.Entities;

namespace WalletApi.Core.Automapper;

public sealed class WalletUserMapping : Profile
{
    public WalletUserMapping()
    {
        CreateMap<WalletUserUpsertModel, WalletUser>()
            .ForMember(walletTransaction => walletTransaction.Transactions,
                options => options.Ignore())
            .ForMember(walletTransaction => walletTransaction.Accounts,
                options => options.Ignore());

        CreateMap<WalletUser, WalletUserViewModel>();
    }
}