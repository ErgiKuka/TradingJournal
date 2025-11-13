using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingJournal.Migrations
{
    /// <inheritdoc />
    public partial class RecycleBinAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaseRef",
                table: "RecoveryCases",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecycleBinItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityType = table.Column<string>(type: "TEXT", nullable: false),
                    EntityKey = table.Column<string>(type: "TEXT", nullable: true),
                    OriginalId = table.Column<int>(type: "INTEGER", nullable: true),
                    PayloadJson = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    DeletedUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiresUtc = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecycleBinItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecycleBinItems_EntityType",
                table: "RecycleBinItems",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_RecycleBinItems_ExpiresUtc",
                table: "RecycleBinItems",
                column: "ExpiresUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecycleBinItems");

            migrationBuilder.DropColumn(
                name: "CaseRef",
                table: "RecoveryCases");
        }
    }
}
