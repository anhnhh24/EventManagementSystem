using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventMedia");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2025, 6, 29, 13, 22, 16, 524, DateTimeKind.Local).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 6, 29, 13, 22, 16, 524, DateTimeKind.Local).AddTicks(7463));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "DateJoined",
                value: new DateTime(2025, 6, 29, 13, 22, 16, 524, DateTimeKind.Local).AddTicks(7466));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventMedia",
                columns: table => new
                {
                    MediaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMedia", x => x.MediaID);
                    table.ForeignKey(
                        name: "FK_EventMedia_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 15, 8, 50, 314, DateTimeKind.Local).AddTicks(514));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 15, 8, 50, 314, DateTimeKind.Local).AddTicks(521));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 15, 8, 50, 314, DateTimeKind.Local).AddTicks(524));

            migrationBuilder.CreateIndex(
                name: "IX_EventMedia_EventID",
                table: "EventMedia",
                column: "EventID");
        }
    }
}
