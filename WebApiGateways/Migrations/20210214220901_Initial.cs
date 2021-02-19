using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiGateways.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GatewaysController",
                columns: table => new
                {
                    SerialNumber = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.SerialNumber);
                });

            migrationBuilder.CreateTable(
                name: "PeripheralGateways",
                columns: table => new
                {
                    GatewaySerialNumber = table.Column<string>(nullable: false),
                    PeripheralId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeripheralGateways", x => new { x.GatewaySerialNumber, x.PeripheralId });
                });

            migrationBuilder.CreateTable(
                name: "Peripherals",
                columns: table => new
                {
                    UID = table.Column<long>(nullable: false),
                    Vendor = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peripherals", x => x.UID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GatewaysController");

            migrationBuilder.DropTable(
                name: "PeripheralGateways");

            migrationBuilder.DropTable(
                name: "Peripherals");
        }
    }
}
