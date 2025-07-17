using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Registrations_RegistrationID",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "EventNotes");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "RegistrationID",
                table: "Payments",
                newName: "BillID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_RegistrationID",
                table: "Payments",
                newName: "IX_Payments_BillID");

            migrationBuilder.AddColumn<int>(
                name: "BillID",
                table: "Registrations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionCode",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "OrderInfo",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillID);
                    table.ForeignKey(
                        name: "FK_Bills_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2025, 7, 16, 16, 30, 42, 529, DateTimeKind.Local).AddTicks(115));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 7, 16, 16, 30, 42, 529, DateTimeKind.Local).AddTicks(119));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "DateJoined",
                value: new DateTime(2025, 7, 16, 16, 30, 42, 529, DateTimeKind.Local).AddTicks(122));

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_BillID",
                table: "Registrations",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserID",
                table: "Bills",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bills_BillID",
                table: "Payments",
                column: "BillID",
                principalTable: "Bills",
                principalColumn: "BillID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Bills_BillID",
                table: "Registrations",
                column: "BillID",
                principalTable: "Bills",
                principalColumn: "BillID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bills_BillID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Bills_BillID",
                table: "Registrations");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_BillID",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "BillID",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "BillID",
                table: "Payments",
                newName: "RegistrationID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BillID",
                table: "Payments",
                newName: "IX_Payments_RegistrationID");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionCode",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderInfo",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EventNotes",
                columns: table => new
                {
                    NoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventNotes", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_EventNotes_Events_EventID",
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
                value: new DateTime(2025, 7, 16, 14, 33, 48, 105, DateTimeKind.Local).AddTicks(3874));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 7, 16, 14, 33, 48, 105, DateTimeKind.Local).AddTicks(3879));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "DateJoined",
                value: new DateTime(2025, 7, 16, 14, 33, 48, 105, DateTimeKind.Local).AddTicks(3882));

            migrationBuilder.CreateIndex(
                name: "IX_EventNotes_EventID",
                table: "EventNotes",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Registrations_RegistrationID",
                table: "Payments",
                column: "RegistrationID",
                principalTable: "Registrations",
                principalColumn: "RegistrationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
