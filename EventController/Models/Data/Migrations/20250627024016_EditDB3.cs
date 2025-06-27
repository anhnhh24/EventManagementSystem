using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Address", "DateJoined", "DoB", "Email", "FullName", "Gender", "Password", "Phone", "ProfileImage" },
                values: new object[] { "123 Admin St, HCMC", new DateTime(2025, 6, 27, 9, 40, 15, 750, DateTimeKind.Local).AddTicks(9089), new DateTime(1992, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.admin@example.com", "Alice Admin", "Female", "P@ssw0rd!", "0901234567", "/img/users/alice.jpg" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "Address", "DateJoined", "DoB", "Email", "FullName", "Gender", "Password", "Phone", "ProfileImage" },
                values: new object[] { "456 Organizer Ave, Da Nang", new DateTime(2025, 6, 27, 9, 40, 15, 750, DateTimeKind.Local).AddTicks(9094), new DateTime(1988, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.organizer@example.com", "Bob Organizer", "Male", "P@ssw0rd!", "0912345678", "/img/users/bob.jpg" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "DateJoined", "DoB", "Email", "FullName", "Gender", "IsEmailVerified", "Password", "Phone", "ProfileImage", "RoleID", "Status" },
                values: new object[] { 3, "789 Participant Rd, Hanoi", new DateTime(2025, 6, 27, 9, 40, 15, 750, DateTimeKind.Local).AddTicks(9097), new DateTime(2000, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "charlie.participant@example.com", "Charlie Participant", "Other", true, "P@ssw0rd!", "0923456789", "/img/users/charlie.jpg", 3, "Active" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DoB",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "DateJoined", "Email", "FullName", "Gender", "Password", "Phone", "ProfileImage" },
                values: new object[] { new DateTime(2025, 6, 27, 9, 21, 59, 213, DateTimeKind.Local).AddTicks(5827), "admin@example.com", "Admin Organizer", "Male", "123456", "0123456789", "/img/users/admin.jpg" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "DateJoined", "Email", "FullName", "Gender", "Password", "Phone", "ProfileImage" },
                values: new object[] { new DateTime(2025, 6, 27, 9, 21, 59, 213, DateTimeKind.Local).AddTicks(5829), "jane@example.com", "Jane Mentee", "Female", "abcdef", "0987654321", "/img/users/jane.jpg" });
        }
    }
}
