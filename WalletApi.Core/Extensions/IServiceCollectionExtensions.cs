using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WalletApi.Core.Repositories;
using WalletApi.Core.Repositories.Interfaces;
using WalletApi.Core.Services;
using WalletApi.Core.Services.Interfaces;

namespace WalletApi.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWalletApiCore(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IWalletAccountRepository, WalletAccountRepository>();
        services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
        services.AddScoped<IWalletUserRepository, WalletUserRepository>();

        services.AddScoped<IWalletTransactionService, WalletTransactionService>();
        services.AddScoped<IWalletAccountService, WalletAccountService>();

        return services;
    }
}