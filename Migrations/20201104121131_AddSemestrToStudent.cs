using Microsoft.EntityFrameworkCore.Migrations;

namespace CW4.Migrations
{
    public partial class AddSemestrToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Semestr",
                table: "Groups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semestr",
                table: "Groups");
        }
    }
}
