using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Users",
                newName: "Gender");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "DateJoined", "Email", "FullName", "Gender", "IsEmailVerified", "Password", "Phone", "ProfileImage" },
                values: new object[] { new DateTime(2025, 6, 27, 9, 21, 59, 213, DateTimeKind.Local).AddTicks(5829), "jane@example.com", "Jane Mentee", "Female", false, "abcdef", "0987654321", "/img/users/jane.jpg" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "DateJoined", "Email", "FullName", "Gender", "IsEmailVerified", "Password", "Phone", "ProfileImage", "RoleID", "Status" },
                values: new object[] { 1, new DateTime(2025, 6, 27, 9, 21, 59, 213, DateTimeKind.Local).AddTicks(5827), "admin@example.com", "Admin Organizer", "Male", true, "123456", "0123456789", "/img/users/admin.jpg", 1, "Active" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "PasswordSalt");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "DateJoined", "Email", "FullName", "IsEmailVerified", "Password", "PasswordSalt", "Phone", "ProfileImage" },
                values: new object[] { new DateTime(2025, 6, 27, 2, 5, 28, 593, DateTimeKind.Local).AddTicks(2035), "organizer@example.com", "Event Organizer", true, "hashed-password", "random-salt", "0123456789", "" });
        }
    }
}
