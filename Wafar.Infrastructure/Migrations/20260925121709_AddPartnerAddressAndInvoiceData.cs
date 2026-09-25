using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wafar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPartnerAddressAndInvoiceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "CouponUsages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InvoiceAmount",
                table: "CouponUsages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetAmount",
                table: "CouponUsages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CouponUsages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "CouponUsages");

            migrationBuilder.DropColumn(
                name: "InvoiceAmount",
                table: "CouponUsages");

            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "CouponUsages");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CouponUsages");
        }
    }
}
