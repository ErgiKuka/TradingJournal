using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingJournal.Migrations
{
    /// <inheritdoc />
    public partial class CleanRecoverySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseType",
                table: "RecoveryCases");

            migrationBuilder.DropColumn(
                name: "AllocatedUSDT",
                table: "RecoveryAllocations");

            migrationBuilder.DropColumn(
                name: "ExitPrice",
                table: "RecoveryAllocations");

            migrationBuilder.DropColumn(
                name: "MarginUSDT",
                table: "RecoveryAllocations");

            migrationBuilder.RenameColumn(
                name: "TradePnL",
                table: "RecoveryAllocations",
                newName: "InvestedUSDT");

            migrationBuilder.AlterColumn<decimal>(
                name: "EntryPrice",
                table: "RecoveryAllocations",
                type: "decimal(18,8)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvestedUSDT",
                table: "RecoveryAllocations",
                newName: "TradePnL");

            migrationBuilder.AddColumn<int>(
                name: "CaseType",
                table: "RecoveryCases",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "EntryPrice",
                table: "RecoveryAllocations",
                type: "decimal(18,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");

            migrationBuilder.AddColumn<decimal>(
                name: "AllocatedUSDT",
                table: "RecoveryAllocations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExitPrice",
                table: "RecoveryAllocations",
                type: "decimal(18,8)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MarginUSDT",
                table: "RecoveryAllocations",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
