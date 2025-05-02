using Microsoft.EntityFrameworkCore.Migrations;

namespace eBilet.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Movies",
                newName: "ImageURL");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Movies",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Movies",
                newName: "Url");
        }
    }
}
