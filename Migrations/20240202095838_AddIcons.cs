using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mastende.Migrations
{
    /// <inheritdoc />
    public partial class AddIcons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LandlordDb");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "LandlordDb",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Bathrooms",
                table: "LandlordDb",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Bedrooms",
                table: "LandlordDb",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<decimal>(
                name: "Garage",
                table: "LandlordDb",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bathrooms",
                table: "LandlordDb");

            migrationBuilder.DropColumn(
                name: "Bedrooms",
                table: "LandlordDb");

            migrationBuilder.DropColumn(
                name: "Garage",
                table: "LandlordDb");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "LandlordDb",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LandlordDb",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
