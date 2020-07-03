using Microsoft.EntityFrameworkCore.Migrations;

namespace SiparisTakip.Migrations
{
    public partial class userPermissionAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userPermission",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userPermission",
                table: "Users");
        }
    }
}