using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class EditDB10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoryID",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Categories");

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

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "SentTime",
                table: "Notifications",
                newName: "SendAt");

            migrationBuilder.RenameColumn(
                name: "NotificationType",
                table: "Notifications",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "Notifications",
                newName: "IsSent");

            migrationBuilder.AlterColumn<int>(
                name: "EventID",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EventCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.CategoryID);
                });

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

            migrationBuilder.InsertData(
                table: "EventCategories",
                columns: new[] { "CategoryID", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Concert", "Live music concerts and shows" },
                    { 2, "Workshop", "Hands-on training & skill-building sessions" },
                    { 3, "Seminar", "Educational seminars and talks" },
                    { 4, "Conference", "Large-scale professional conferences" },
                    { 5, "Marathon", "Running & endurance sport events" },
                    { 6, "Food Festival", "Culinary fairs and tasting events" },
                    { 7, "Art Exhibition", "Galleries and art showcases" },
                    { 8, "Startup Pitch", "Entrepreneurial pitch & demo days" },
                    { 9, "Charity Event", "Fund-raising & community service" },
                    { 10, "Movie Night", "Indoor / outdoor film screenings" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                columns: new[] { "Description", "Location", "OrganizerID" },
                values: new object[] { "Top V-Pop artists live on stage.", "Grand Hall, District 1", 2008 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                columns: new[] { "Description", "Location", "OrganizerID", "Title" },
                values: new object[] { "Hands-on HTML / CSS / JS & React in 5 days.", "Đà Nẵng Tech Park", 2008, "Front-end Dev Bootcamp" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                columns: new[] { "CategoryID", "Description", "EndTime", "ImageUrl", "Location", "MaxAttendees", "OrganizerID", "StartTime", "Title", "VenueID" },
                values: new object[] { 4, "Latest AI research & enterprise applications.", new DateTime(2025, 11, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/ai-conf.jpg", "SECC, District 7", 1500, 2008, new DateTime(2025, 11, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "AI Conference 2025", 3 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "DateJoined", "DoB", "Email", "FullName", "Gender", "IsEmailVerified", "Password", "Phone", "ProfileImage", "RoleID", "Status" },
                values: new object[,]
                {
                    { 1, "123 Admin St, HCMC", new DateTime(2025, 7, 2, 15, 41, 56, 626, DateTimeKind.Local).AddTicks(4488), new DateTime(1992, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.admin@example.com", "Alice Admin", "Female", true, "P@ssw0rd!", "0901234567", "/img/users/alice.jpg", 1, "Active" },
                    { 2, "456 Organizer Ave, Da Nang", new DateTime(2025, 7, 2, 15, 41, 56, 626, DateTimeKind.Local).AddTicks(4493), new DateTime(1988, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.organizer@example.com", "Bob Organizer", "Male", false, "P@ssw0rd!", "0912345678", "/img/users/bob.jpg", 2, "Active" },
                    { 3, "789 Participant Rd, Hanoi", new DateTime(2025, 7, 2, 15, 41, 56, 626, DateTimeKind.Local).AddTicks(4495), new DateTime(2000, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "charlie.participant@example.com", "Charlie Participant", "Other", true, "P@ssw0rd!", "0923456789", "/img/users/charlie.jpg", 3, "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventNotes_EventID",
                table: "EventNotes",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventCategories_CategoryID",
                table: "Events",
                column: "CategoryID",
                principalTable: "EventCategories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventCategories_CategoryID",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "EventNotes");

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

            migrationBuilder.RenameColumn(
                name: "SendAt",
                table: "Notifications",
                newName: "SentTime");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Notifications",
                newName: "NotificationType");

            migrationBuilder.RenameColumn(
                name: "IsSent",
                table: "Notifications",
                newName: "IsRead");

            migrationBuilder.AlterColumn<int>(
                name: "EventID",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Events",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "Description", "Icon" },
                values: new object[,]
                {
                    { 1, "Concert", "Live music concerts and shows", "bi-music-note-beamed" },
                    { 2, "Workshop", "Hands-on training & skill-building sessions", "bi-tools" },
                    { 3, "Seminar", "Educational seminars and talks", "bi-mic" },
                    { 4, "Conference", "Large-scale professional conferences", "bi-people" },
                    { 5, "Marathon", "Running & endurance sport events", "bi-running" },
                    { 6, "Food Festival", "Culinary fairs and tasting events", "bi-egg-fried" },
                    { 7, "Art Exhibition", "Galleries and art showcases", "bi-brush" },
                    { 8, "Startup Pitch", "Entrepreneurial pitch & demo days", "bi-lightbulb" },
                    { 9, "Charity Event", "Fund-raising & community service", "bi-hand-heart" },
                    { 10, "Movie Night", "Indoor / outdoor film screenings", "bi-film" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                columns: new[] { "Description", "Location", "OrganizerID", "Price" },
                values: new object[] { "Top V‑Pop artists live on stage.", "Grand Hall, District 1", 2008, 1990000L });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                columns: new[] { "Description", "Location", "OrganizerID", "Price", "Title" },
                values: new object[] { "Hands‑on HTML / CSS / JS & React in 5 days.", "Đà Nẵng Tech Park", 2008, 3200000L, "Front‑end Dev Bootcamp" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                columns: new[] { "CategoryID", "Description", "EndTime", "ImageUrl", "Location", "MaxAttendees", "OrganizerID", "Price", "StartTime", "Title", "VenueID" },
                values: new object[] { 3, "An evening talk on mindfulness & wellbeing.", new DateTime(2025, 10, 12, 21, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/mindfulness.jpg", "Riverside Hotel, HCM", 300, 2008, 250000L, new DateTime(2025, 10, 12, 19, 0, 0, 0, DateTimeKind.Unspecified), "Mindfulness Seminar", 1 });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueID", "Address", "Capacity", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 4, "Sept 23 Park, D1, HCM", 10000, "Central outdoor park—ideal for food festivals & fairs.", "/img/venues/sept23park.jpg", "September 23 Park" },
                    { 5, "Crescent Lake, Phú Mỹ Hưng, D7", 1500, "Scenic lakeside venue for outdoor movie nights.", "/img/venues/crescent-lake.jpg", "Crescent Lake Park" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "CategoryID", "Description", "EndTime", "ImageUrl", "Location", "MaxAttendees", "OrganizerID", "Price", "StartTime", "Status", "Title", "VenueID" },
                values: new object[,]
                {
                    { 4, 4, "Latest AI research & enterprise applications.", new DateTime(2025, 11, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/ai-conf.jpg", "SECC, District 7", 1500, 2008, 4500000L, new DateTime(2025, 11, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "Upcoming", "AI Conference 2025", 3 },
                    { 5, 5, "5 km charity run for children's hospitals.", new DateTime(2025, 10, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/charity-run.jpg", "Thảo Cầm Viên, HCM", 5000, 2008, 150000L, new DateTime(2025, 10, 5, 6, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Charity Run 2025", 1 },
                    { 6, 6, "Taste 100+ dishes from local vendors.", new DateTime(2025, 8, 22, 22, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/food-fest.jpg", "September 23 Park", 8000, 2008, 100000L, new DateTime(2025, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Active", "HCM Street Food Fest", 1 },
                    { 7, 7, "Showcase of contemporary Vietnamese artists.", new DateTime(2025, 7, 30, 20, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/art-expo.jpg", "Fine Arts Museum, HCM", 300, 2008, 80000L, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Modern Art Expo", 1 },
                    { 8, 8, "Pitch session for early‑stage startups.", new DateTime(2025, 9, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/demo-day.jpg", "Đà Nẵng Tech Park", 200, 2008, 0L, new DateTime(2025, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Upcoming", "Startup Demo Day", 2 },
                    { 9, 9, "Fund‑raising dinner with live auction.", new DateTime(2025, 12, 12, 22, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/gala.jpg", "Saigon Opera House", 400, 2008, 3500000L, new DateTime(2025, 12, 12, 18, 30, 0, 0, DateTimeKind.Unspecified), "Upcoming", "Gala Dinner for Hope", 1 },
                    { 10, 10, "Enjoy a classic under the stars.", new DateTime(2025, 6, 28, 22, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/movie-night.jpg", "Crescent Lake Park, D7", 1000, 2008, 120000L, new DateTime(2025, 6, 28, 19, 30, 0, 0, DateTimeKind.Unspecified), "Active", "Outdoor Movie Night – Classic Hits", 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoryID",
                table: "Events",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
