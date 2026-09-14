using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Wafar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RewardCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "صيانة" },
                    { 5, "أجهزة" },
                    { 6, "إكسسوارات" }
                });

            migrationBuilder.InsertData(
                table: "Rewards",
                columns: new[] { "Id", "AccessoryId", "CreatedAt", "Description", "DeviceId", "DiscountValue", "ExpirationDays", "IsActive", "MaintenanceServiceId", "ProbabilityPercentage", "RewardCategoryId", "RewardName", "RewardType", "SparePartId" },
                values: new object[,]
                {
                    { 5, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 7, true, null, 100m, 4, "فحص مجاني للجهاز", (byte)6, null },
                    { 6, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 15m, 7, true, null, 100m, 5, "خصم 15% على الأجهزة", (byte)1, null },
                    { 7, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 14, true, null, 100m, 6, "اسكرينة حماية مجانية", (byte)4, null }
                });

            migrationBuilder.InsertData(
                table: "QRCodeRewards",
                columns: new[] { "Id", "ProbabilityOverride", "QRCodeId", "RewardId" },
                values: new object[,]
                {
                    { 5, null, 1, 5 },
                    { 6, null, 1, 6 },
                    { 7, null, 1, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QRCodeRewards",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "QRCodeRewards",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "QRCodeRewards",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rewards",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rewards",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rewards",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RewardCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RewardCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RewardCategories",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
