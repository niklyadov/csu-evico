using Evico.Entity;
using Microsoft.EntityFrameworkCore;

namespace Evico;

public class ApplicationContext : DbContext
{
    public virtual DbSet<EntityRecord> EntityRecords { get; set; }
    public virtual DbSet<PlaceRecord> PlaceRecords { get; set; }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}