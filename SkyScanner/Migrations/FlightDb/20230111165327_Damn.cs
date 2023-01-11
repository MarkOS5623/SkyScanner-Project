using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyScanner.Migrations.FlightDb
{
    /// <inheritdoc />
    public partial class Damn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlightBack",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightBack",
                table: "Flights");
        }
    }
}
