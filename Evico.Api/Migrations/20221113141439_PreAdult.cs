using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Evico.Api.Migrations
{
    public partial class PreAdult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Photos_PhotoId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Photos_PhotoId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Photos_PhotoId",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Places_TempId1",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Events_PhotoId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TempId1",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "PhotoId",
                table: "Places",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Places_PhotoId",
                table: "Places",
                newName: "IX_Places_ParentId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Profiles",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Profiles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Profiles",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Profiles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "Role",
                table: "Profiles",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "VkUserId",
                table: "Profiles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Places",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "Places",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Events",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "Events",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "EventCategoryRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategoryRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventReviewRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: true),
                    Rate = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventReviewRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventReviewRecord_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventReviewRecord_Profiles_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceCategoryRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceCategoryRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceReviewRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: true),
                    Rate = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceReviewRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceReviewRecord_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceReviewRecord_Profiles_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventCategoryRecordEventRecord",
                columns: table => new
                {
                    CategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    EventsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategoryRecordEventRecord", x => new { x.CategoriesId, x.EventsId });
                    table.ForeignKey(
                        name: "FK_EventCategoryRecordEventRecord_EventCategoryRecord_Categorie~",
                        column: x => x.CategoriesId,
                        principalTable: "EventCategoryRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCategoryRecordEventRecord_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceCategoryRecordPlaceRecord",
                columns: table => new
                {
                    CategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    PlacesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceCategoryRecordPlaceRecord", x => new { x.CategoriesId, x.PlacesId });
                    table.ForeignKey(
                        name: "FK_PlaceCategoryRecordPlaceRecord_PlaceCategoryRecord_Categorie~",
                        column: x => x.CategoriesId,
                        principalTable: "PlaceCategoryRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceCategoryRecordPlaceRecord_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true),
                    MinioBucket = table.Column<int>(type: "int", nullable: false),
                    MinioInternalId = table.Column<Guid>(type: "char(36)", nullable: false),
                    EventReviewRecordId = table.Column<long>(type: "bigint", nullable: true),
                    PhotoType = table.Column<int>(type: "int", nullable: false),
                    PlaceReviewRecordId = table.Column<long>(type: "bigint", nullable: true),
                    EventId = table.Column<long>(type: "bigint", nullable: true),
                    ReviewId = table.Column<long>(type: "bigint", nullable: true),
                    PlaceId = table.Column<long>(type: "bigint", nullable: true),
                    PlaceReviewPhotoRecord_ReviewId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_EventReviewRecord_EventReviewRecordId",
                        column: x => x.EventReviewRecordId,
                        principalTable: "EventReviewRecord",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photo_EventReviewRecord_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "EventReviewRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photo_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photo_PlaceReviewRecord_PlaceReviewPhotoRecord_ReviewId",
                        column: x => x.PlaceReviewPhotoRecord_ReviewId,
                        principalTable: "PlaceReviewRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photo_PlaceReviewRecord_PlaceReviewRecordId",
                        column: x => x.PlaceReviewRecordId,
                        principalTable: "PlaceReviewRecord",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photo_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photo_Profiles_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_Name",
                table: "Profiles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Places_OwnerId",
                table: "Places",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OwnerId",
                table: "Events",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCategoryRecordEventRecord_EventsId",
                table: "EventCategoryRecordEventRecord",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReviewRecord_AuthorId",
                table: "EventReviewRecord",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReviewRecord_EventId",
                table: "EventReviewRecord",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_AuthorId",
                table: "Photo",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_EventId",
                table: "Photo",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_EventReviewRecordId",
                table: "Photo",
                column: "EventReviewRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_MinioInternalId",
                table: "Photo",
                column: "MinioInternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_PlaceId",
                table: "Photo",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_PlaceReviewPhotoRecord_ReviewId",
                table: "Photo",
                column: "PlaceReviewPhotoRecord_ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_PlaceReviewRecordId",
                table: "Photo",
                column: "PlaceReviewRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_ReviewId",
                table: "Photo",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceCategoryRecordPlaceRecord_PlacesId",
                table: "PlaceCategoryRecordPlaceRecord",
                column: "PlacesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceReviewRecord_AuthorId",
                table: "PlaceReviewRecord",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceReviewRecord_PlaceId",
                table: "PlaceReviewRecord",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Profiles_OwnerId",
                table: "Events",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Places_ParentId",
                table: "Places",
                column: "ParentId",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Profiles_OwnerId",
                table: "Places",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Photo_PhotoId",
                table: "Profiles",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Profiles_OwnerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Places_ParentId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Profiles_OwnerId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Photo_PhotoId",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "EventCategoryRecordEventRecord");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "PlaceCategoryRecordPlaceRecord");

            migrationBuilder.DropTable(
                name: "EventCategoryRecord");

            migrationBuilder.DropTable(
                name: "EventReviewRecord");

            migrationBuilder.DropTable(
                name: "PlaceReviewRecord");

            migrationBuilder.DropTable(
                name: "PlaceCategoryRecord");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_Name",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Places_OwnerId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Events_OwnerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "VkUserId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Places",
                newName: "PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_Places_ParentId",
                table: "Places",
                newName: "IX_Places_PhotoId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Profiles",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<int>(
                name: "TempId1",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PhotoId",
                table: "Events",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Places_TempId1",
                table: "Places",
                column: "TempId1");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EventRecordId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PlaceRecordId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Events_EventRecordId",
                        column: x => x.EventRecordId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Places_PlaceRecordId",
                        column: x => x.PlaceRecordId,
                        principalTable: "Places",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Profiles_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PlaceRecordId = table.Column<long>(type: "bigint", nullable: true),
                    ReviewRecordId = table.Column<long>(type: "bigint", nullable: true),
                    Uri = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Places_PlaceRecordId",
                        column: x => x.PlaceRecordId,
                        principalTable: "Places",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photos_Reviews_ReviewRecordId",
                        column: x => x.ReviewRecordId,
                        principalTable: "Reviews",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_PhotoId",
                table: "Events",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PlaceRecordId",
                table: "Photos",
                column: "PlaceRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ReviewRecordId",
                table: "Photos",
                column: "ReviewRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AuthorId",
                table: "Reviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EventRecordId",
                table: "Reviews",
                column: "EventRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PlaceRecordId",
                table: "Reviews",
                column: "PlaceRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Photos_PhotoId",
                table: "Events",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Photos_PhotoId",
                table: "Places",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Photos_PhotoId",
                table: "Profiles",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}
