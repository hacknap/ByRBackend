using Microsoft.EntityFrameworkCore.Migrations;

namespace ByR.Migrations
{
    public partial class ultimaBaseDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagen64portada",
                table: "Property",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imageurl",
                table: "Property",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagen64portada",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "imageurl",
                table: "Property");
        }
    }
}
