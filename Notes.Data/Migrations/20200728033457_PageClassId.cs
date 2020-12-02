using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Data.Migrations
{
    public partial class PageClassId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassId",
                table: "NotesPage",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotesPage_ClassId",
                table: "NotesPage",
                column: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotesPage_ClassId",
                table: "NotesPage");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "NotesPage");
        }
    }
}
