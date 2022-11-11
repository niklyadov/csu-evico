using Evico.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Evico.Api;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    public DbSet<CategoryRecord> Categories { get; set; } = default!;
    public DbSet<PlaceRecord> Places { get; set; } = default!;
    public DbSet<EventRecord> Events { get; set; } = default!;
    public DbSet<ReviewRecord> Reviews { get; set; } = default!;
    public DbSet<ProfileRecord> Profiles { get; set; } = default!;
    public DbSet<PhotoRecord> Photos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventRecord>()
            .HasOne(x => x.Owner)
            .WithMany(y => y.OwnEvents);

        modelBuilder.Entity<EventRecord>()
            .HasMany(x => x.Photos)
            .WithOne(y => y.Event);
        
        modelBuilder.Entity<PlaceRecord>()
            .HasOne(x => x.Owner)
            .WithMany(y => y.OwnPlaces);

        modelBuilder.Entity<PlaceRecord>()
            .HasMany(x => x.Photos)
            .WithOne(y => y.Place);
        
        modelBuilder.Entity<EventReviewRecord>()
            .HasOne(x => x.Author)
            .WithMany(y => y.OwnEventReviews);

        modelBuilder.Entity<PlaceReviewRecord>()
            .HasOne(x => x.Author)
            .WithMany(y => y.OwnPlaceReviews);

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

        modelBuilder.Entity<ProfileRecord>()
            .HasIndex(x => x.Name)
            .IsUnique();
        
        modelBuilder.Entity<ProfileRecord>()
            .HasOne(x => x.Photo)
            .WithOne(y => y.Profile);
        
        modelBuilder.Entity<PhotoRecord>()
            .HasIndex(x => x.MinioInternalId)
            .IsUnique();

        modelBuilder.Entity<PhotoRecord>()
            .ToTable("Photo")
            .HasDiscriminator<int>("PhotoType")
            .HasValue<PhotoRecord>(0)
            .HasValue<PlacePhotoRecord>(10)
            .HasValue<EventPhotoRecord>(20)
            .HasValue<ProfilePhotoRecord>(30)
            .HasValue<PlaceReviewPhotoRecord>(40)
            .HasValue<EventReviewPhotoRecord>(50);

        base.OnModelCreating(modelBuilder);
    }
}