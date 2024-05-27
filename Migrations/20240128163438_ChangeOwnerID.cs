using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mastende.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOwnerID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "LandlordDb");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "LandlordDb",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "LandlordDb");

            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "LandlordDb",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
