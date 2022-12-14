// <auto-generated />
using System;
using Evico.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Evico.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20221023191934_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EventRecordProfileRecord", b =>
                {
                    b.Property<long>("OrganizerEventsId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrganizersId")
                        .HasColumnType("bigint");

                    b.HasKey("OrganizerEventsId", "OrganizersId");

                    b.HasIndex("OrganizersId");

                    b.ToTable("EventRecordProfileRecord");
                });

            modelBuilder.Entity("EventRecordProfileRecord1", b =>
                {
                    b.Property<long>("ParticipantEventsId")
                        .HasColumnType("bigint");

                    b.Property<long>("ParticipantsId")
                        .HasColumnType("bigint");

                    b.HasKey("ParticipantEventsId", "ParticipantsId");

                    b.HasIndex("ParticipantsId");

                    b.ToTable("EventRecordProfileRecord1");
                });

            modelBuilder.Entity("Evico.Api.Entity.EventRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("PhotoId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlaceId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.HasIndex("PlaceId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Evico.Api.Entity.ExternalPhoto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("PlaceRecordId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ReviewRecordId")
                        .HasColumnType("bigint");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PlaceRecordId");

                    b.HasIndex("ReviewRecordId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Evico.Api.Entity.PlaceRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("LocationLatitude")
                        .HasColumnType("double");

                    b.Property<double>("LocationLongitude")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("PhotoId")
                        .HasColumnType("bigint");

                    b.Property<int>("TempId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("TempId1");

                    b.HasIndex("PhotoId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("Evico.Api.Entity.ProfileRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("PhotoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Evico.Api.Entity.ReviewRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("EventRecordId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("PlaceRecordId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("EventRecordId");

                    b.HasIndex("PlaceRecordId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("EventRecordProfileRecord", b =>
                {
                    b.HasOne("Evico.Api.Entity.EventRecord", null)
                        .WithMany()
                        .HasForeignKey("OrganizerEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Evico.Api.Entity.ProfileRecord", null)
                        .WithMany()
                        .HasForeignKey("OrganizersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventRecordProfileRecord1", b =>
                {
                    b.HasOne("Evico.Api.Entity.EventRecord", null)
                        .WithMany()
                        .HasForeignKey("ParticipantEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Evico.Api.Entity.ProfileRecord", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Evico.Api.Entity.EventRecord", b =>
                {
                    b.HasOne("Evico.Api.Entity.ExternalPhoto", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.HasOne("Evico.Api.Entity.PlaceRecord", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photo");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("Evico.Api.Entity.ExternalPhoto", b =>
                {
                    b.HasOne("Evico.Api.Entity.PlaceRecord", null)
                        .WithMany("Photos")
                        .HasForeignKey("PlaceRecordId");

                    b.HasOne("Evico.Api.Entity.ReviewRecord", null)
                        .WithMany("Photos")
                        .HasForeignKey("ReviewRecordId");
                });

            modelBuilder.Entity("Evico.Api.Entity.PlaceRecord", b =>
                {
                    b.HasOne("Evico.Api.Entity.ExternalPhoto", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("Evico.Api.Entity.ProfileRecord", b =>
                {
                    b.HasOne("Evico.Api.Entity.ExternalPhoto", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("Evico.Api.Entity.ReviewRecord", b =>
                {
                    b.HasOne("Evico.Api.Entity.ProfileRecord", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Evico.Api.Entity.EventRecord", null)
                        .WithMany("Reviews")
                        .HasForeignKey("EventRecordId");

                    b.HasOne("Evico.Api.Entity.PlaceRecord", null)
                        .WithMany("Reviews")
                        .HasForeignKey("PlaceRecordId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Evico.Api.Entity.EventRecord", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Evico.Api.Entity.PlaceRecord", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Evico.Api.Entity.ReviewRecord", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
