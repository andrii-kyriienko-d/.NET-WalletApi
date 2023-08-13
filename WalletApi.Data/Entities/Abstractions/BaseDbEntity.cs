namespace WalletApi.Data.Entities.Abstractions;

public abstract class BaseDbEntity
{
    public virtual Guid Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime? ModifiedDate { get; set; }
}