using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyScanner.Migrations
{
    /// <inheritdoc />
    public partial class Sorry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    KeepLoggedIn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserID = table.Column<string>(name: "User_ID", type: "nvarchar(4)", nullable: false),
                    BookedSeats = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TwoWay = table.Column<bool>(type: "bit", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    FlightId = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_User_ID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    UserID = table.Column<string>(name: "User_ID", type: "nvarchar(4)", nullable: false),
                    IsraeliID = table.Column<string>(name: "Israeli_ID", type: "nvarchar(9)", maxLength: 9, nullable: false),
                    ExpMonth = table.Column<int>(type: "int", nullable: false),
                    ExpYear = table.Column<int>(type: "int", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    Save = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CardNumber);
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_User_ID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_User_ID",
                table: "Bookings",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_User_ID",
                table: "CreditCards",
                column: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
