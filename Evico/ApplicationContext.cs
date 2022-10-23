using Evico.Entity;
using Microsoft.EntityFrameworkCore;

namespace Evico;

public class ApplicationContext : DbContext
{
    public virtual DbSet<EntityRecord> Entities { get; set; } = default!;
    public virtual DbSet<PlaceRecord> Places { get; set; } = default!;
    public virtual DbSet<ReviewRecord> Reviews { get; set; } = default!;
    public virtual DbSet<ProfileRecord> Profiles { get; set; } = default!;
    public virtual DbSet<EventCategoryRecord> EventCategories { get; set; } = default!;
    public virtual DbSet<PlaceCategoryRecord> PlaceCategories { get; set; } = default!;
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}