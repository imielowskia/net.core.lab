using Microsoft.EntityFrameworkCore.Migrations;

namespace CW4.Migrations
{
    public partial class RemoveDataInGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Grades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Data",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
