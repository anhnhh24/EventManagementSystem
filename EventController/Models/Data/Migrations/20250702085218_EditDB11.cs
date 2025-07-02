using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Events",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                column: "Price",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                column: "Price",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                column: "Price",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2025, 7, 2, 15, 52, 18, 64, DateTimeKind.Local).AddTicks(5690));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 7, 2, 15, 52, 18, 64, DateTimeKind.Local).AddTicks(5695));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "DateJoined",
                value: new DateTime(2025, 7, 2, 15, 52, 18, 64, DateTimeKind.Local).AddTicks(5698));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2025, 7, 2, 15, 41, 56, 626, DateTimeKind.Local).AddTicks(4488));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 7, 2, 15, 41, 56, 626, DateTimeKind.Local).AddTicks(4493));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "DateJoined",
                value: new DateTime(2025, 7, 2, 15, 41, 56, 626, DateTimeKind.Local).AddTicks(4495));
        }
    }
}
