using Microsoft.EntityFrameworkCore.Migrations;

namespace SiparisTakip.Migrations
{
    public partial class pdfandexcel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "requestExcel",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "requestPDF",
                table: "Requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requestExcel",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "requestPDF",
                table: "Requests");
        }
    }
}
