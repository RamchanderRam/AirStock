using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirStock.Migrations
{
    public partial class StockDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FleetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverKm = table.Column<int>(type: "int", nullable: false),
                    DateOfLastService = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });

            migrationBuilder.CreateTable(
                name: "JobCards",
                columns: table => new
                {
                    JobCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BriefDescriptionOfWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpareForService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCardQuantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    FittedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChiefMechanicSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCards", x => x.JobCardId);
                    table.ForeignKey(
                        name: "FK_JobCards_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    SparePartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SparePartCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SparePartAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.SparePartId);
                    table.ForeignKey(
                        name: "FK_SpareParts_JobCards_JobCardId",
                        column: x => x.JobCardId,
                        principalTable: "JobCards",
                        principalColumn: "JobCardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobCards_VehicleId",
                table: "JobCards",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_JobCardId",
                table: "SpareParts",
                column: "JobCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "NewProduct");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserModel");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "JobCards");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
