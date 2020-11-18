using Microsoft.EntityFrameworkCore.Migrations;

namespace Github_backend.Migrations
{
    public partial class AddHomepageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "homepage",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "homepage",
                table: "Projects");
        }
    }
}
