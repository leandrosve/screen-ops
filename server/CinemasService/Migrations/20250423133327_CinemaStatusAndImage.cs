using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemasService.Migrations
{
    /// <inheritdoc />
    public partial class CinemaStatusAndImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LayoutElements_PositionX_PositionY",
                table: "LayoutElements");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Cinemas");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cinemas",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cinemas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LayoutElements_PositionX",
                table: "LayoutElements",
                column: "PositionX");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutElements_PositionY",
                table: "LayoutElements",
                column: "PositionY");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LayoutElements_PositionX",
                table: "LayoutElements");

            migrationBuilder.DropIndex(
                name: "IX_LayoutElements_PositionY",
                table: "LayoutElements");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cinemas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Rooms",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "Rooms",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Cinemas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_LayoutElements_PositionX_PositionY",
                table: "LayoutElements",
                columns: new[] { "PositionX", "PositionY" });
        }
    }
}
