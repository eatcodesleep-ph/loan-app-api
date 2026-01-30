using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LoanApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 28, 14, 20, 37, 380, DateTimeKind.Utc).AddTicks(8211),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 28, 13, 12, 16, 615, DateTimeKind.Utc).AddTicks(8759));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LoanApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 28, 13, 12, 16, 615, DateTimeKind.Utc).AddTicks(8759),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 28, 14, 20, 37, 380, DateTimeKind.Utc).AddTicks(8211));
        }
    }
}
