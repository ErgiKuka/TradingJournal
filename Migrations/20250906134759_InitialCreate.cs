using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingJournal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Symbol = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    TradeType = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    EntryPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ExitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    StopLoss = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TakeProfit = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Margin = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ProfitLoss = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ScreenshotLink = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ScreenshotImage = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trades");
        }
    }
}
