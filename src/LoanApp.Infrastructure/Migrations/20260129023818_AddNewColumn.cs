using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanApplications",
                table: "LoanApplications");

            migrationBuilder.RenameTable(
                name: "LoanApplications",
                newName: "LoanApplication");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LoanApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 29, 2, 38, 17, 910, DateTimeKind.Utc).AddTicks(8038),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 28, 14, 20, 37, 380, DateTimeKind.Utc).AddTicks(8211));

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "LoanApplication",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstablishmentFee",
                table: "LoanApplication",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RepaymentAmount",
                table: "LoanApplication",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInterest",
                table: "LoanApplication",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanApplication",
                table: "LoanApplication",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RepaymentSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoanApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallmentNumber = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RepaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Principal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepaymentSchedule", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepaymentSchedule_LoanApplicationId_InstallmentNumber",
                table: "RepaymentSchedule",
                columns: new[] { "LoanApplicationId", "InstallmentNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepaymentSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanApplication",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "EstablishmentFee",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "RepaymentAmount",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "TotalInterest",
                table: "LoanApplication");

            migrationBuilder.RenameTable(
                name: "LoanApplication",
                newName: "LoanApplications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LoanApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 28, 14, 20, 37, 380, DateTimeKind.Utc).AddTicks(8211),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 29, 2, 38, 17, 910, DateTimeKind.Utc).AddTicks(8038));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanApplications",
                table: "LoanApplications",
                column: "Id");
        }
    }
}
