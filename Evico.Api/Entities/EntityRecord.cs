namespace Evico.Api.Entity;

public abstract record EntityRecord : IEntity
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDateTime { get; set; } = null;
}