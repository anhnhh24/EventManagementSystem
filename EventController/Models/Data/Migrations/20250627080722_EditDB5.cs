using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 15, 7, 21, 400, DateTimeKind.Local).AddTicks(8763));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 15, 7, 21, 400, DateTimeKind.Local).AddTicks(8769));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 15, 7, 21, 400, DateTimeKind.Local).AddTicks(8771));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 12, 5, 16, 240, DateTimeKind.Local).AddTicks(3097));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 12, 5, 16, 240, DateTimeKind.Local).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 12, 5, 16, 240, DateTimeKind.Local).AddTicks(3104));
        }
    }
}
