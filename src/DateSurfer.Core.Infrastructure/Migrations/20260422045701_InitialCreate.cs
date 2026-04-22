using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateSurfer.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "FeeRules",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Country",
                value: "Germany");

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 2,
                column: "Country",
                value: "Germany");

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 3,
                column: "Country",
                value: "Germany");

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 4,
                column: "Country",
                value: "USA");

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 5,
                column: "Country",
                value: "USA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.AlterColumn<int>(
                name: "Country",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Country",
                table: "FeeRules",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Country",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 2,
                column: "Country",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 3,
                column: "Country",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 4,
                column: "Country",
                value: 3);

            migrationBuilder.UpdateData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 5,
                column: "Country",
                value: 3);

            migrationBuilder.InsertData(
                table: "FeeRules",
                columns: new[] { "Id", "Country", "IsActive", "MaxAge", "MembershipType", "MinAge", "MonthlyFee" },
                values: new object[] { 6, 3, true, 99, 2, 18, 69.99m });
        }
    }
}
