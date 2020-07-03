using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SiparisTakip.Migrations
{
    public partial class adddatabasetoallDateString2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "requestCreateAt",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "requestCreateAt",
                table: "Requests",
                type: "Date",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}