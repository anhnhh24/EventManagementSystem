using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Venue_VenueID",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Venue",
                table: "Venue");

            migrationBuilder.RenameTable(
                name: "Venue",
                newName: "Venues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Venues",
                table: "Venues",
                column: "VenueID");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 2, 5, 28, 593, DateTimeKind.Local).AddTicks(2035));

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Venues_VenueID",
                table: "Events",
                column: "VenueID",
                principalTable: "Venues",
                principalColumn: "VenueID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Venues_VenueID",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Venues",
                table: "Venues");

            migrationBuilder.RenameTable(
                name: "Venues",
                newName: "Venue");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Venue",
                table: "Venue",
                column: "VenueID");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 1, 52, 27, 965, DateTimeKind.Local).AddTicks(690));

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Venue_VenueID",
                table: "Events",
                column: "VenueID",
                principalTable: "Venue",
                principalColumn: "VenueID");
        }
    }
}
