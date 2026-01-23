using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesForReservation1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDay",
                table: "Villas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Reservations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Villas");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Reservations");
        }
    }
}
