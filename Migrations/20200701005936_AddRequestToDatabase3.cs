using Microsoft.EntityFrameworkCore.Migrations;

namespace SiparisTakip.Migrations
{
    public partial class AddRequestToDatabase3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "requestUser",
                table: "Request",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requestUser",
                table: "Request");
        }
    }
}