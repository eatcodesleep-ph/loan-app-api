using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnProductType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductType",
                table: "LoanApplication",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LoanApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 29, 14, 28, 30, 828, DateTimeKind.Utc).AddTicks(5239),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 29, 14, 16, 25, 24, DateTimeKind.Utc).AddTicks(138));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductType",
                table: "LoanApplication",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LoanApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 29, 14, 16, 25, 24, DateTimeKind.Utc).AddTicks(138),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 29, 14, 28, 30, 828, DateTimeKind.Utc).AddTicks(5239));
        }
    }
}
