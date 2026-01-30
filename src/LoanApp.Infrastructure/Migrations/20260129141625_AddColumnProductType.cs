using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnProductType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LoanApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 29, 14, 16, 25, 24, DateTimeKind.Utc).AddTicks(138),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 29, 2, 38, 17, 910, DateTimeKind.Utc).AddTicks(8038));

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "LoanApplication",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "LoanApplication");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LoanApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 29, 2, 38, 17, 910, DateTimeKind.Utc).AddTicks(8038),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 29, 14, 16, 25, 24, DateTimeKind.Utc).AddTicks(138));
        }
    }
}
