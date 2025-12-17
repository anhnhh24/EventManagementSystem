using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventController.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "EmailVerificationTokens",
                columns: table => new
                {
                    TokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerificationTokens", x => x.TokenID);
                    table.ForeignKey(
                        name: "FK_EmailVerificationTokens_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VenueID = table.Column<int>(type: "int", nullable: true),
                    OrganizerID = table.Column<int>(type: "int", nullable: false),
                    MaxAttendees = table.Column<int>(type: "int", nullable: true),
                    CurrentAttendees = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_EventCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "EventCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Users_OrganizerID",
                        column: x => x.OrganizerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "VenueID");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Bills_BillID",
                        column: x => x.BillID,
                        principalTable: "Bills",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BillID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationID);
                    table.ForeignKey(
                        name: "FK_Registrations_Bills_BillID",
                        column: x => x.BillID,
                        principalTable: "Bills",
                        principalColumn: "BillID");
                    table.ForeignKey(
                        name: "FK_Registrations_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Organizer" },
                    { 3, "Participant" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueID", "Address", "Capacity", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "123 Lê Lợi, Quận 1, HCM", 2000, "Premium indoor concert hall in downtown Saigon.", "/img/venues/grand-hall.jpg", "Grand Hall" },
                    { 2, "Hòa Khánh, Đà Nẵng", 500, "Modern coworking & workshop space for tech events.", "/img/venues/dn-techpark.jpg", "Đà Nẵng Tech Park" },
                    { 3, "799 Nguyễn Văn Linh, Quận 7, HCM", 8000, "Largest expo hall in Ho Chi Minh City.", "/img/venues/secc.jpg", "SECC – Exhibition Center" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "DateJoined", "DoB", "Email", "FullName", "Gender", "IsEmailVerified", "Password", "Phone", "ProfileImage", "RoleID", "Status" },
                values: new object[,]
                {
                    { 1, "123 Admin St, HCMC", new DateTime(2025, 12, 17, 0, 33, 13, 417, DateTimeKind.Local).AddTicks(6037), new DateTime(1992, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.admin@example.com", "Alice Admin", "Female", true, "P@ssw0rd!", "0901234567", "/img/users/alice.jpg", 1, "Active" },
                    { 2, "456 Organizer Ave, Da Nang", new DateTime(2025, 12, 17, 0, 33, 13, 417, DateTimeKind.Local).AddTicks(6043), new DateTime(1988, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.organizer@example.com", "Bob Organizer", "Male", false, "P@ssw0rd!", "0912345678", "/img/users/bob.jpg", 2, "Active" },
                    { 3, "789 Participant Rd, Hanoi", new DateTime(2025, 12, 17, 0, 33, 13, 417, DateTimeKind.Local).AddTicks(6046), new DateTime(2000, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "charlie.participant@example.com", "Charlie Participant", "Other", true, "P@ssw0rd!", "0923456789", "/img/users/charlie.jpg", 3, "Active" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "CategoryID", "CreatedAt", "CurrentAttendees", "Description", "EndTime", "ImageUrl", "MaxAttendees", "OrganizerID", "Price", "StartTime", "Status", "Title", "VenueID" },
                values: new object[,]
                {
                    { 2, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "An evening talk on mindfulness & wellbeing.", new DateTime(2025, 10, 12, 21, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/mindfulness.jpg", 300, 2, 1900000L, new DateTime(2025, 10, 12, 19, 0, 0, 0, DateTimeKind.Unspecified), "Upcoming", "Mindfulness Seminar", 1 },
                    { 3, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Latest AI research & enterprise applications.", new DateTime(2025, 11, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/ai-conf.jpg", 1500, 2, 1900000L, new DateTime(2025, 11, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "Upcoming", "AI Conference 2025", 3 },
                    { 4, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5 km charity run for children's hospitals.", new DateTime(2025, 10, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/charity-run.jpg", 5000, 2, 1900000L, new DateTime(2025, 10, 5, 6, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Charity Run 2025", 1 },
                    { 5, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Taste 100+ dishes from local vendors.", new DateTime(2025, 8, 22, 22, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/food-fest.jpg", 8000, 2, 1900000L, new DateTime(2025, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Active", "HCM Street Food Fest", 1 },
                    { 6, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Showcase of contemporary Vietnamese artists.", new DateTime(2025, 7, 30, 20, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/art-expo.jpg", 300, 2, 1900000L, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Modern Art Expo", 1 },
                    { 7, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pitch session for early‑stage startups.", new DateTime(2025, 9, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/demo-day.jpg", 200, 2, 1900000L, new DateTime(2025, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Upcoming", "Startup Demo Day", 2 },
                    { 8, 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fund‑raising dinner with live auction.", new DateTime(2025, 12, 12, 22, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/gala.jpg", 400, 2, 1900000L, new DateTime(2025, 12, 12, 18, 30, 0, 0, DateTimeKind.Unspecified), "Upcoming", "Gala Dinner for Hope", 1 },
                    { 9, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Enjoy a classic under the stars.", new DateTime(2025, 6, 28, 22, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/movie-night.jpg", 1000, 2, 1900000L, new DateTime(2025, 6, 28, 19, 30, 0, 0, DateTimeKind.Unspecified), "Active", "Outdoor Movie Night – Classic Hits", 1 },
                    { 10, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Learn to solve problems creatively.", new DateTime(2025, 8, 25, 17, 0, 0, 0, DateTimeKind.Unspecified), "/img/events/design-thinking.jpg", 60, 2, 1900000L, new DateTime(2025, 8, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Design Thinking Workshop", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserID",
                table: "Bills",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationTokens_UserID",
                table: "EmailVerificationTokens",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryID",
                table: "Events",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerID",
                table: "Events",
                column: "OrganizerID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueID",
                table: "Events",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_EventID",
                table: "Notifications",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillID",
                table: "Payments",
                column: "BillID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_BillID",
                table: "Registrations",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventID",
                table: "Registrations",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserID",
                table: "Registrations",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailVerificationTokens");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
