using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Data.Migrations
{
    public partial class addCatalog3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    IndentityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ParentCode = table.Column<string>(nullable: true),
                    Code = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.IndentityId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_Deleted",
                table: "Catalogs",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_Id",
                table: "Catalogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_ParentCode",
                table: "Catalogs",
                column: "ParentCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalogs");
        }
    }
}
