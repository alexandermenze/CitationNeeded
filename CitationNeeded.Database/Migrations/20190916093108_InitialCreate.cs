using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CitationNeeded.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitationBooks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitationBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CitationGroup",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CitationBookId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitationGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitationGroup_User_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CitationGroup_CitationBooks_CitationBookId",
                        column: x => x.CitationBookId,
                        principalTable: "CitationBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Citation",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SpeakerId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    CitationGroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citation_CitationGroup_CitationGroupId",
                        column: x => x.CitationGroupId,
                        principalTable: "CitationGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citation_User_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citation_CitationGroupId",
                table: "Citation",
                column: "CitationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Citation_SpeakerId",
                table: "Citation",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_CitationGroup_AuthorId",
                table: "CitationGroup",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CitationGroup_CitationBookId",
                table: "CitationGroup",
                column: "CitationBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citation");

            migrationBuilder.DropTable(
                name: "CitationGroup");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "CitationBooks");
        }
    }
}
