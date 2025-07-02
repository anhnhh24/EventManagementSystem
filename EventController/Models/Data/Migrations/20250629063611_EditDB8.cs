using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Events");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "DateJoined", "DoB", "Email", "FullName", "Gender", "IsEmailVerified", "Password", "Phone", "ProfileImage", "RoleID", "Status" },
                values: new object[,]
                {
                    { 1, "123 Admin St, HCMC", new DateTime(2025, 6, 29, 13, 22, 16, 524, DateTimeKind.Local).AddTicks(7459), new DateTime(1992, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.admin@example.com", "Alice Admin", "Female", true, "P@ssw0rd!", "0901234567", "/img/users/alice.jpg", 1, "Active" },
                    { 2, "456 Organizer Ave, Da Nang", new DateTime(2025, 6, 29, 13, 22, 16, 524, DateTimeKind.Local).AddTicks(7463), new DateTime(1988, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.organizer@example.com", "Bob Organizer", "Male", false, "P@ssw0rd!", "0912345678", "/img/users/bob.jpg", 2, "Active" },
                    { 3, "789 Participant Rd, Hanoi", new DateTime(2025, 6, 29, 13, 22, 16, 524, DateTimeKind.Local).AddTicks(7466), new DateTime(2000, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "charlie.participant@example.com", "Charlie Participant", "Other", true, "P@ssw0rd!", "0923456789", "/img/users/charlie.jpg", 3, "Active" }
                });
        }
    }
}
