using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Data.Migrations
{
    public partial class extentionAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NotesId",
                table: "NotesPage",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NotesId",
                table: "NotesContent",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SectionId",
                table: "NotesContent",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Notes",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Notes",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotesPage_NotesId",
                table: "NotesPage",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesContent_NotesId",
                table: "NotesContent",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesContent_SectionId",
                table: "NotesContent",
                column: "SectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotesPage_NotesId",
                table: "NotesPage");

            migrationBuilder.DropIndex(
                name: "IX_NotesContent_NotesId",
                table: "NotesContent");

            migrationBuilder.DropIndex(
                name: "IX_NotesContent_SectionId",
                table: "NotesContent");

            migrationBuilder.DropColumn(
                name: "NotesId",
                table: "NotesPage");

            migrationBuilder.DropColumn(
                name: "NotesId",
                table: "NotesContent");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "NotesContent");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Notes");
        }
    }
}
