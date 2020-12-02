using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Data.Migrations
{
    public partial class addCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NotesSection",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NotesPage",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotesSection_NotesId",
                table: "NotesSection",
                column: "NotesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotesSection_NotesId",
                table: "NotesSection");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NotesSection",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NotesPage",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
