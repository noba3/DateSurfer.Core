using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DateSurfer.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUSAFeeRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FeeRules",
                columns: new[] { "Id", "Country", "IsActive", "MaxAge", "MembershipType", "MinAge", "MonthlyFee" },
                values: new object[,]
                {
                    { 4, 3, true, 99, 0, 18, 12.99m },
                    { 5, 3, true, 99, 1, 18, 34.99m },
                    { 6, 3, true, 99, 2, 18, 69.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FeeRules",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
