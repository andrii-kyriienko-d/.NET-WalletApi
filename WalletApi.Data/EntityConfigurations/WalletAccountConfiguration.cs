using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletApi.Data.Entities;

namespace WalletApi.Data.EntityConfigurations;

internal sealed class WalletAccountConfiguration : BaseDbEntityConfiguration<WalletAccount>
{
    public override void Configure(EntityTypeBuilder<WalletAccount> builder)
    {
        base.Configure(builder);
        builder.ToTable(nameof(WalletAccount));
        
        builder.Property(walletAccount => walletAccount.Name)
            .HasColumnName(nameof(WalletAccount.Name))
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(walletAccount => walletAccount.Balance)
            .HasColumnName(nameof(WalletAccount.Balance));
        
        builder.Property(walletAccount => walletAccount.DailyPoints)
            .HasColumnName(nameof(WalletAccount.DailyPoints));
        
        builder.HasOne(walletAccount => walletAccount.Owner)
            .WithMany(walletUser => walletUser.Accounts)
            .HasForeignKey(wallerUser => wallerUser.OwnerId)
            .IsRequired();
    }
}