using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletApi.Data.Entities.Abstractions;

namespace WalletApi.Data.EntityConfigurations;

internal class BaseDbEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseDbEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(k => k.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName(nameof(BaseDbEntity.Id));
        
        builder.Property(p => p.CreatedDate)
            .HasColumnName(nameof(BaseDbEntity.CreatedDate))
            .IsRequired();
        
        builder.Property(p => p.ModifiedDate)
            .HasColumnName(nameof(BaseDbEntity.ModifiedDate));
    }
}