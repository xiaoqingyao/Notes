using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Data.Migrations
{
    public partial class IndentityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesSection",
                table: "NotesSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesPage",
                table: "NotesPage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesContent",
                table: "NotesContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "IndentityId",
                table: "NotesSection",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "IndentityId",
                table: "NotesPage",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "IndentityId",
                table: "NotesContent",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "IndentityId",
                table: "Notes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesSection",
                table: "NotesSection",
                column: "IndentityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesPage",
                table: "NotesPage",
                column: "IndentityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesContent",
                table: "NotesContent",
                column: "IndentityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "IndentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesSection",
                table: "NotesSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesPage",
                table: "NotesPage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesContent",
                table: "NotesContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IndentityId",
                table: "NotesSection");

            migrationBuilder.DropColumn(
                name: "IndentityId",
                table: "NotesPage");

            migrationBuilder.DropColumn(
                name: "IndentityId",
                table: "NotesContent");

            migrationBuilder.DropColumn(
                name: "IndentityId",
                table: "Notes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesSection",
                table: "NotesSection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesPage",
                table: "NotesPage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesContent",
                table: "NotesContent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");
        }
    }
}
