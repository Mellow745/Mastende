using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mastende.Migrations
{
    /// <inheritdoc />
    public partial class AllowToUploadPicsAndDocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentsFile",
                table: "LandlordDb",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentsFile",
                table: "LandlordDb");
        }
    }
}
