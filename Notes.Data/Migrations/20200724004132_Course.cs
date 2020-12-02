using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Data.Migrations
{
    public partial class Course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotesForCourse",
                columns: table => new
                {
                    IndentityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DsId = table.Column<Guid>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    SectionId = table.Column<Guid>(nullable: false),
                    PageId = table.Column<Guid>(nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesForCourse", x => x.IndentityId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotesForCourse_Deleted",
                table: "NotesForCourse",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_NotesForCourse_DsId",
                table: "NotesForCourse",
                column: "DsId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesForCourse_Id",
                table: "NotesForCourse",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotesForCourse");
        }
    }
}
