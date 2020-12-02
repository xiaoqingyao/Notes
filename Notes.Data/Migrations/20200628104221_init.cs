using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ClassId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Catelog = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    CalelogCode = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotesContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    PageId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotesPage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    SectionId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesPage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotesSection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    NotesId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesSection", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CreatorId",
                table: "Notes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Deleted",
                table: "Notes",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Id",
                table: "Notes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NotesContent_Deleted",
                table: "NotesContent",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_NotesContent_Id",
                table: "NotesContent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NotesContent_PageId",
                table: "NotesContent",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesPage_Deleted",
                table: "NotesPage",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_NotesPage_Id",
                table: "NotesPage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NotesPage_SectionId",
                table: "NotesPage",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesSection_Deleted",
                table: "NotesSection",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_NotesSection_Id",
                table: "NotesSection",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "NotesContent");

            migrationBuilder.DropTable(
                name: "NotesPage");

            migrationBuilder.DropTable(
                name: "NotesSection");
        }
    }
}
