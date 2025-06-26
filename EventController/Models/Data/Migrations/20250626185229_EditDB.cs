using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "VenueType",
                table: "Venue",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "ContactInfo",
                table: "Venue",
                newName: "Description");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                column: "Description",
                value: "Top V-Pop artists live on stage.");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                columns: new[] { "Description", "Location" },
                values: new object[] { "Hands-on HTML / CSS / JS & React in 5 days.", "Đà Nẵng Tech Park" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                columns: new[] { "Description", "Location" },
                values: new object[] { "Latest AI research & enterprise applications.", "SECC, District 7" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 1, 52, 27, 965, DateTimeKind.Local).AddTicks(690));

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "VenueID",
                keyValue: 1,
                columns: new[] { "Address", "Description", "Image" },
                values: new object[] { "123 Lê Lợi, Quận 1, HCM", "Premium indoor concert hall in downtown Saigon.", "/img/venues/grand-hall.jpg" });

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "VenueID",
                keyValue: 2,
                columns: new[] { "Address", "Description", "Image", "Name" },
                values: new object[] { "Hòa Khánh, Đà Nẵng", "Modern coworking & workshop space for tech events.", "/img/venues/dn-techpark.jpg", "Đà Nẵng Tech Park" });

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "VenueID",
                keyValue: 3,
                columns: new[] { "Address", "Description", "Image", "Name" },
                values: new object[] { "799 Nguyễn Văn Linh, Quận 7, HCM", "Largest expo hall in Ho Chi Minh City.", "/img/venues/secc.jpg", "SECC – Exhibition Center" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Venue",
                newName: "VenueType");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Venue",
                newName: "ContactInfo");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Venue",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Venue",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                columns: new[] { "Description", "IsPublic", "Tags", "TimeZone" },
                values: new object[] { "A night of hit songs with top V-Pop artists.", true, "music,live,vpop", "Asia/Ho_Chi_Minh" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                columns: new[] { "Description", "IsPublic", "Location", "Tags", "TimeZone" },
                values: new object[] { "Hands-on HTML/CSS/JS & React in 5 days.", true, "Da Nang Tech Park", "coding,frontend,react", "Asia/Ho_Chi_Minh" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                columns: new[] { "Description", "IsPublic", "Location", "Tags", "TimeZone" },
                values: new object[] { "Three-day conference covering AI trends & applications.", true, "SECC – District 7, HCMC", "AI,ML,conference", "Asia/Ho_Chi_Minh" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "CategoryID", "Description", "EndTime", "ImageUrl", "IsPublic", "Location", "MaxAttendees", "OrganizerID", "StartTime", "Status", "Tags", "TimeZone", "Title", "VenueID" },
                values: new object[,]
                {
                    { 4, 2, "Learn to solve problems creatively with design thinking.", new DateTime(2025, 8, 25, 17, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/design-thinking.jpg", true, "Indochina Riverside, Da Nang", 60, 2, new DateTime(2025, 8, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), "Active", "design,creativity,workshop", "Asia/Ho_Chi_Minh", "Design Thinking Workshop", 2 },
                    { 5, 5, "5 km charity run to raise funds for children’s hospitals.", new DateTime(2025, 10, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/charity-run.jpg", true, "Thao Cam Vien, HCMC", 5000, 2, new DateTime(2025, 10, 5, 6, 0, 0, 0, DateTimeKind.Unspecified), "Active", "charity,run,health", "Asia/Ho_Chi_Minh", "Charity Run for Hope", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 6, 27, 1, 31, 33, 165, DateTimeKind.Local).AddTicks(6146));

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "VenueID",
                keyValue: 1,
                columns: new[] { "Address", "ContactInfo", "Latitude", "Longitude", "VenueType" },
                values: new object[] { "123 Le Loi St, District 1, Ho Chi Minh City", "grandhall@example.com | +84 28 1234 5678", 10.7769m, 106.7009m, "Indoor" });

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "VenueID",
                keyValue: 2,
                columns: new[] { "Address", "ContactInfo", "Latitude", "Longitude", "Name", "VenueType" },
                values: new object[] { "Lot B2, Hoa Khanh Industrial Zone, Da Nang", "techpark@dn.com | +84 236 987 654", 16.0544m, 108.2022m, "Danang Tech Park", "Conference Center" });

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "VenueID",
                keyValue: 3,
                columns: new[] { "Address", "ContactInfo", "Latitude", "Longitude", "Name", "VenueType" },
                values: new object[] { "799 Nguyen Van Linh, District 7, Ho Chi Minh City", "info@secc.com.vn | +84 28 5413 5999", 10.7269m, 106.7218m, "SECC – Saigon Exhibition & Convention Center", "Expo Hall" });
        }
    }
}
