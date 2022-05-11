using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRental.Data.Migrations
{
    public partial class AddImageUrlToCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgaUrl",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgaUrl",
                table: "Cars");
        }
    }
}
