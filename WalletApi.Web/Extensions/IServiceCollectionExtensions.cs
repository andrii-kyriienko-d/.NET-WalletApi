using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace WalletApi.Web.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWalletApiWeb(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}