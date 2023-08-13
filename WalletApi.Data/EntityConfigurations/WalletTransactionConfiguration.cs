using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletApi.Data.Entities;
using WalletApi.Data.Enums;

namespace WalletApi.Data.EntityConfigurations;

internal sealed class WalletTransactionConfiguration : BaseDbEntityConfiguration<WalletTransaction>
{
    public override void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        base.Configure(builder);
        
        builder.ToTable(nameof(WalletTransaction));
        
        builder.Property(walletTransaction => walletTransaction.Title)
            .HasColumnName(nameof(WalletTransaction.Title))
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(walletTransaction => walletTransaction.Description)
            .HasColumnName(nameof(WalletTransaction.Description))
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(walletTransaction => walletTransaction.Sum)
            .HasColumnName(nameof(WalletTransaction.Sum))
            .IsRequired();

        builder.Property(walletTransaction => walletTransaction.IsPending)
            .HasColumnName(nameof(WalletTransaction.IsPending))
            .IsRequired();
        
        builder.Property(walletTransaction => walletTransaction.Type)
            .HasColumnName(nameof(WalletTransaction.Type))
            .HasDefaultValue(TransactionType.Payment)
            .IsRequired();

        builder.HasOne(walletTransaction => walletTransaction.User)
            .WithMany(walletUser => walletUser.Transactions)
            .HasForeignKey(walletTransaction => walletTransaction.UserId);
        
        builder.HasOne(walletTransaction => walletTransaction.Account)
            .WithMany(walletAccount => walletAccount.Transactions)
            .HasForeignKey(walletTransaction => walletTransaction.AccountId);
    }
}