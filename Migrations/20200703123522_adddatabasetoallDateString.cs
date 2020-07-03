using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SiparisTakip.Migrations
{
    public partial class adddatabasetoallDateString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "requestCreateAt",
                table: "Requests",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "requestNo",
                table: "Requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requestCreateAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "requestNo",
                table: "Requests");
        }
    }
}