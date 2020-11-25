using Microsoft.EntityFrameworkCore.Migrations;

namespace CW4.Migrations
{
    public partial class AddGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false),
                    GroupID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false),
                    Ocena = table.Column<int>(nullable: false),
                    Data = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => new { x.StudentID, x.CourseID, x.GroupID });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
