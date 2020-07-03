using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SiparisTakip.Migrations
{
    public partial class AddRequestToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    requestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    requestDepartment = table.Column<string>(nullable: true),
                    requestSteff = table.Column<string>(nullable: true),
                    requestProject = table.Column<string>(nullable: true),
                    requestExpensePlace = table.Column<string>(nullable: true),
                    requestProductFeatures = table.Column<string>(nullable: true),
                    requestDescription = table.Column<string>(nullable: true),
                    requestQuantity = table.Column<int>(nullable: false),
                    requestSpecies = table.Column<int>(nullable: false),
                    requestImage = table.Column<string>(nullable: true),
                    requestEstimatedPrice = table.Column<float>(nullable: false),
                    requestDeliveryDate = table.Column<DateTime>(nullable: false),
                    requestSupplyCompany1 = table.Column<string>(nullable: true),
                    requestSupplyCompany2 = table.Column<string>(nullable: true),
                    requestSupplyCompany3 = table.Column<string>(nullable: true),
                    userId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.requestId);
                    table.ForeignKey(
                        name: "FK_Request_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_userId",
                table: "Request",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Request");
        }
    }
}