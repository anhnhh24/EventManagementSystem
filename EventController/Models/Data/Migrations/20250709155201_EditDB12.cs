using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                columns: new[] { "CategoryID", "Description", "EndTime", "ImageUrl", "Location", "MaxAttendees", "StartTime", "Status", "Title", "VenueID" },
                values: new object[] { 3, "An evening talk on mindfulness & wellbeing.", new DateTime(2025, 10, 12, 21, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/mindfulness.jpg", "Riverside Hotel, HCM", 300, new DateTime(2025, 10, 12, 19, 0, 0, 0, DateTimeKind.Unspecified), "Upcoming", "Mindfulness Seminar", 1 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "CategoryID", "Description", "EndTime", "ImageUrl", "Location", "MaxAttendees", "OrganizerID", "Price", "StartTime", "Status", "Title", "VenueID" },
                values: new object[,]
                {
                    { 4, 5, "5 km charity run for children's hospitals.", new DateTime(2025, 10, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/charity-run.jpg", "Thảo Cầm Viên, HCM", 5000, 2, 0L, new DateTime(2025, 10, 5, 6, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Charity Run 2025", 1 },
                    { 5, 6, "Taste 100+ dishes from local vendors.", new DateTime(2025, 8, 22, 22, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/food-fest.jpg", "September 23 Park", 8000, 2, 0L, new DateTime(2025, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Active", "HCM Street Food Fest", 1 },
                    { 6, 7, "Showcase of contemporary Vietnamese artists.", new DateTime(2025, 7, 30, 20, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/art-expo.jpg", "Fine Arts Museum, HCM", 300, 2, 0L, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Modern Art Expo", 1 },
                    { 7, 8, "Pitch session for early‑stage startups.", new DateTime(2025, 9, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/demo-day.jpg", "Đà Nẵng Tech Park", 200, 2, 0L, new DateTime(2025, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Upcoming", "Startup Demo Day", 2 },
                    { 8, 9, "Fund‑raising dinner with live auction.", new DateTime(2025, 12, 12, 22, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/gala.jpg", "Saigon Opera House", 400, 2, 0L, new DateTime(2025, 12, 12, 18, 30, 0, 0, DateTimeKind.Unspecified), "Upcoming", "Gala Dinner for Hope", 1 },
                    { 9, 10, "Enjoy a classic under the stars.", new DateTime(2025, 6, 28, 22, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/movie-night.jpg", "Crescent Lake Park, D7", 1000, 2, 0L, new DateTime(2025, 6, 28, 19, 30, 0, 0, DateTimeKind.Unspecified), "Active", "Outdoor Movie Night – Classic Hits", 1 },
                    { 10, 2, "Learn to solve problems creatively.", new DateTime(2025, 8, 25, 17, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/design-thinking.jpg", "Indochina Riverside, Đà Nẵng", 60, 2, 0L, new DateTime(2025, 8, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Design Thinking Workshop", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2025, 7, 9, 22, 51, 59, 649, DateTimeKind.Local).AddTicks(183));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "DateJoined",
                value: new DateTime(2025, 7, 9, 22, 51, 59, 649, DateTimeKind.Local).AddTicks(189));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "DateJoined",
                value: new DateTime(2025, 7, 9, 22, 51, 59, 649, DateTimeKind.Local).AddTicks(191));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                columns: new[] { "CategoryID", "Description", "EndTime", "ImageUrl", "Location", "MaxAttendees", "StartTime", "Status", "Title", "VenueID" },
                values: new object[] { 2, "Hands-on HTML / CSS / JS & React in 5 days.", new DateTime(2025, 9, 22, 17, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/frontend-bootcamp.jpg", "Đà Nẵng Tech Park", 120, new DateTime(2025, 9, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Front-end Dev Bootcamp", 2 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "CategoryID", "Description", "EndTime", "ImageUrl", "Location", "MaxAttendees", "OrganizerID", "Price", "StartTime", "Status", "Title", "VenueID" },
                values: new object[] { 1, 1, "Top V-Pop artists live on stage.", new DateTime(2025, 9, 1, 23, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/music-night.jpg", "Grand Hall, District 1", 2000, 2, 0L, new DateTime(2025, 9, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), "Active", "HCMC Live Music Night", 1 });

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
    }
}
