using Microsoft.EntityFrameworkCore;
using WalletApi.Data.Entities;

namespace WalletApi.Data.DatabaseContexts;

public sealed class WalletDbContext : DbContext
{
    public WalletDbContext(DbContextOptions options) : base(options){ }

    public DbSet<WalletAccount> WalletAccounts { get; set; }
    public DbSet<WalletUser> WalletUsers { get; set; }
    public DbSet<WalletTransaction> WalletTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(WalletDbContext).Assembly);
    }
}