using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Evico.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventRecordProfileRecord",
                columns: table => new
                {
                    OrganizerEventsId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRecordProfileRecord", x => new { x.OrganizerEventsId, x.OrganizersId });
                });

            migrationBuilder.CreateTable(
                name: "EventRecordProfileRecord1",
                columns: table => new
                {
                    ParticipantEventsId = table.Column<long>(type: "bigint", nullable: false),
                    ParticipantsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRecordProfileRecord1", x => new { x.ParticipantEventsId, x.ParticipantsId });
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PhotoId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Uri = table.Column<string>(type: "longtext", nullable: false),
                    PlaceRecordId = table.Column<long>(type: "bigint", nullable: true),
                    ReviewRecordId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LocationLatitude = table.Column<double>(type: "double", nullable: false),
                    LocationLongitude = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    PhotoId = table.Column<long>(type: "bigint", nullable: true),
                    TempId1 = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.UniqueConstraint("AK_Places_TempId1", x => x.TempId1);
                    table.ForeignKey(
                        name: "FK_Places_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Lastname = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    PhotoId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: false),
                    EventRecordId = table.Column<long>(type: "bigint", nullable: true),
                    PlaceRecordId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_EventRecordProfileRecord_OrganizersId",
                table: "EventRecordProfileRecord",
                column: "OrganizersId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRecordProfileRecord1_ParticipantsId",
                table: "EventRecordProfileRecord1",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PhotoId",
                table: "Events",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PlaceId",
                table: "Events",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PlaceRecordId",
                table: "Photos",
                column: "PlaceRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ReviewRecordId",
                table: "Photos",
                column: "ReviewRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_PhotoId",
                table: "Places",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PhotoId",
                table: "Profiles",
                column: "PhotoId");

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
                name: "FK_EventRecordProfileRecord_Events_OrganizerEventsId",
                table: "EventRecordProfileRecord",
                column: "OrganizerEventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRecordProfileRecord_Profiles_OrganizersId",
                table: "EventRecordProfileRecord",
                column: "OrganizersId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRecordProfileRecord1_Events_ParticipantEventsId",
                table: "EventRecordProfileRecord1",
                column: "ParticipantEventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRecordProfileRecord1_Profiles_ParticipantsId",
                table: "EventRecordProfileRecord1",
                column: "ParticipantsId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Photos_PhotoId",
                table: "Events",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Places_PlaceId",
                table: "Events",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Places_PlaceRecordId",
                table: "Photos",
                column: "PlaceRecordId",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Reviews_ReviewRecordId",
                table: "Photos",
                column: "ReviewRecordId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Events_EventRecordId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Profiles_AuthorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Photos_PhotoId",
                table: "Places");

            migrationBuilder.DropTable(
                name: "EventRecordProfileRecord");

            migrationBuilder.DropTable(
                name: "EventRecordProfileRecord1");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
