using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mastende.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIDCapitalLetters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "LandlordDb",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LandlordDb",
                newName: "UserID");
        }
    }
}
