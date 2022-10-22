namespace Evico.Entity;

public interface IEntity
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}

public abstract record EntityRecord : IEntity
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}