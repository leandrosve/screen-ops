using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemasService.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalsToLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccesibleSeats",
                table: "Layouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisabledSeats",
                table: "Layouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StandardSeats",
                table: "Layouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VipSeats",
                table: "Layouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccesibleSeats",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "DisabledSeats",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "StandardSeats",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "VipSeats",
                table: "Layouts");
        }
    }
}
