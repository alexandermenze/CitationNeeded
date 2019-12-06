using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CitationNeeded.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    HashedPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

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
                name: "AccountVerifications",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccountId = table.Column<string>(nullable: true),
                    VerificationToken = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountVerifications_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_CitationGroup_Accounts_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Accounts",
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
                    Speaker = table.Column<string>(nullable: true),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountVerifications_AccountId",
                table: "AccountVerifications",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Citation_CitationGroupId",
                table: "Citation",
                column: "CitationGroupId");

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
                name: "AccountVerifications");

            migrationBuilder.DropTable(
                name: "Citation");

            migrationBuilder.DropTable(
                name: "CitationGroup");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CitationBooks");
        }
    }
}
