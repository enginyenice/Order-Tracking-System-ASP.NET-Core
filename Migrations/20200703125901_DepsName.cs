using Microsoft.EntityFrameworkCore.Migrations;

namespace SiparisTakip.Migrations
{
    public partial class DepsName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Deps",
                table: "Deps");

            migrationBuilder.RenameTable(
                name: "Deps",
                newName: "Departments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "depId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Deps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deps",
                table: "Deps",
                column: "depId");
        }
    }
}