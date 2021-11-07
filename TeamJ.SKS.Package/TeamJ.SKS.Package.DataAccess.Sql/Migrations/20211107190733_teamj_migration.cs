using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamJ.SKS.Package.DataAccess.Sql.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class teamj_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DALRecipient",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DALRecipient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hops",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HopType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessingDelayMins = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    RegionGeoJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogisticsPartner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogisticsPartnerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DALTruck_RegionGeoJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberPlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    TrackingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.TrackingId);
                    table.ForeignKey(
                        name: "FK_Parcels_DALRecipient_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "DALRecipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_DALRecipient_SenderId",
                        column: x => x.SenderId,
                        principalTable: "DALRecipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DALWarehouseNextHops",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TraveltimeMins = table.Column<int>(type: "int", nullable: false),
                    HopId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DALWarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DALWarehouseNextHops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DALWarehouseNextHops_Hops_DALWarehouseId",
                        column: x => x.DALWarehouseId,
                        principalTable: "Hops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DALWarehouseNextHops_Hops_HopId",
                        column: x => x.HopId,
                        principalTable: "Hops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DALHopArrival",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DALParcelTrackingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DALParcelTrackingId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DALHopArrival", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DALHopArrival_Parcels_DALParcelTrackingId",
                        column: x => x.DALParcelTrackingId,
                        principalTable: "Parcels",
                        principalColumn: "TrackingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DALHopArrival_Parcels_DALParcelTrackingId1",
                        column: x => x.DALParcelTrackingId1,
                        principalTable: "Parcels",
                        principalColumn: "TrackingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DALHopArrival_DALParcelTrackingId",
                table: "DALHopArrival",
                column: "DALParcelTrackingId");

            migrationBuilder.CreateIndex(
                name: "IX_DALHopArrival_DALParcelTrackingId1",
                table: "DALHopArrival",
                column: "DALParcelTrackingId1");

            migrationBuilder.CreateIndex(
                name: "IX_DALWarehouseNextHops_DALWarehouseId",
                table: "DALWarehouseNextHops",
                column: "DALWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_DALWarehouseNextHops_HopId",
                table: "DALWarehouseNextHops",
                column: "HopId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_RecipientId",
                table: "Parcels",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_SenderId",
                table: "Parcels",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DALHopArrival");

            migrationBuilder.DropTable(
                name: "DALWarehouseNextHops");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Hops");

            migrationBuilder.DropTable(
                name: "DALRecipient");
        }
    }
}
