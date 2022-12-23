using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyScanner.Migrations.FlightDb
{
    /// <inheritdoc />
    public partial class UpdateFlightsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Airline = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatNum = table.Column<string>(name: "Seat_Num", type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Flightnum = table.Column<string>(name: "Flight_num", type: "nvarchar(4)", nullable: false),
                    Booked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatNum);
                    table.ForeignKey(
                        name: "FK_Seats_Flights_Flight_num",
                        column: x => x.Flightnum,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_Flight_num",
                table: "Seats",
                column: "Flight_num");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
