using Microsoft.EntityFrameworkCore.Migrations;

namespace ByR.Migrations
{
    public partial class bddEnimg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagen64",
                table: "Gallery",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagen64",
                table: "Gallery");
        }
    }
}
