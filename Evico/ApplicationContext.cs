using Evico.Entity;
using Microsoft.EntityFrameworkCore;

namespace Evico;

public class ApplicationContext : DbContext
{
    public virtual DbSet<EntityRecord> Entities { get; set; }
    public virtual DbSet<PlaceRecord> Places { get; set; }
    public virtual DbSet<ReviewRecord> Reviews { get; set; }
    public virtual DbSet<ProfileRecord> Profiles { get; set; }
    public virtual DbSet<EventCategoryRecord> EventCategories { get; set; }
    public virtual DbSet<PlaceCategoryRecord> PlaceCategories { get; set; }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}