using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletApi.Data.Entities;

namespace WalletApi.Data.EntityConfigurations;

internal sealed class WalletUserConfiguration : BaseDbEntityConfiguration<WalletUser>
{
    public override void Configure(EntityTypeBuilder<WalletUser> builder)
    {
        base.Configure(builder);
        
        builder.ToTable(nameof(WalletUser));
        
        builder.Property(walletUser => walletUser.Name)
            .HasColumnName(nameof(WalletUser.Name))
            .HasMaxLength(256)
            .IsRequired();
    }
}