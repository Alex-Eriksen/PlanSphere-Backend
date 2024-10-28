namespace Domain.Entities.Base;

public interface ISoftDeletedEntity
{
    public DateTime? DeletedAt { get; set; }
    public ulong? DeletedBy { get;}
}