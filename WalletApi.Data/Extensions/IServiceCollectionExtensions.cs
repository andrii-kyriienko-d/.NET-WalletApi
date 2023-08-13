using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WalletApi.Data.DatabaseContexts;

namespace WalletApi.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWalletContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<WalletDbContext>(options =>
        {
            options.UseNpgsql(connectionString, builder => builder.MigrationsAssembly("WalletApi"));
        });

        return services;
    }
}