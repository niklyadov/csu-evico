using Evico.Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Evico.Api;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<PlaceRecord> Places { get; set; } = default!;
    public DbSet<PlaceCategoryRecord> PlaceCategories { get; set; } = default!;

    public DbSet<EventRecord> Events { get; set; } = default!;
    public DbSet<EventCategoryRecord> EventCategories { get; set; } = default!;

    public DbSet<ReviewRecord> Reviews { get; set; } = default!;
    public DbSet<ProfileRecord> Profiles { get; set; } = default!;
    public DbSet<ExternalPhoto> Photos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<EventRecord>()
        //    .HasOne(x => x.Owner)
        //    .WithMany(y => y.OwnEvents);

        //modelBuilder.Entity<PlaceRecord>()
        //    .HasOne(x => x.Owner)
        //    .WithMany(y => y.OwnPlaces);

        modelBuilder.Entity<EventRecord>()
            .HasMany(x => x.Organizers)
            .WithMany(y => y.OrganizerEvents);

        modelBuilder.Entity<EventRecord>()
            .HasMany(x => x.Participants)
            .WithMany(y => y.ParticipantEvents);

        modelBuilder.Entity<EventRecord>()
            .HasMany(x => x.Reviews)
            .WithOne(y => y.Event);

        modelBuilder.Entity<PlaceRecord>()
            .HasMany(x => x.Reviews)
            .WithOne(y => y.Place);

        modelBuilder.Entity<EventRecord>()
            .HasMany(x => x.Categories)
            .WithMany(y => y.Events);

        modelBuilder.Entity<PlaceRecord>()
            .HasMany(x => x.Categories)
            .WithMany(y => y.Places);

        base.OnModelCreating(modelBuilder);
    }
}