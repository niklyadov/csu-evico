using Evico.Entity;
using Microsoft.EntityFrameworkCore;

namespace Evico;

public sealed class ApplicationContext : DbContext
{
    public DbSet<PlaceRecord> Places { get; set; } = default!;
    public DbSet<PlaceCategoryRecord> PlaceCategories { get; set; } = default!;
    
    public DbSet<EventRecord> Events { get; set; } = default!;
    public DbSet<EventCategoryRecord> EventCategories { get; set; } = default!;
    
    public DbSet<ReviewRecord> Reviews { get; set; } = default!;
    public DbSet<ProfileRecord> Profiles { get; set; } = default!;
    public DbSet<ExternalPhoto> Photos { get; set; } = default!;
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventRecord>()
            .HasOne(x => x.Owner)
            .WithMany(y => y.OwnEvents);
        
        modelBuilder.Entity<EventRecord>()
            .HasMany(x => x.Organizers)
            .WithMany(y => y.OrganizerEvents);
        
        modelBuilder.Entity<EventRecord>()
            .HasMany(x => x.Participants)
            .WithMany(y => y.ParticipantEvents);

        modelBuilder.Entity<PlaceRecord>()
            .HasOne(x => x.Owner)
            .WithMany(y => y.OwnPlaces);


        base.OnModelCreating(modelBuilder);
    }
}