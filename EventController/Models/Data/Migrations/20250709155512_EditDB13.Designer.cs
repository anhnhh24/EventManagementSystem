﻿// <auto-generated />
using System;
using EventController.Models.Data.DBcontext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventController.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20250709155512_EditDB13")]
    partial class EditDB13
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxAttendees")
                        .HasColumnType("int");

                    b.Property<int>("OrganizerID")
                        .HasColumnType("int");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VenueID")
                        .HasColumnType("int");

                    b.HasKey("EventID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("OrganizerID");

                    b.HasIndex("VenueID");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventID = 2,
                            CategoryID = 3,
                            Description = "An evening talk on mindfulness & wellbeing.",
                            EndTime = new DateTime(2025, 10, 12, 21, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "/img/events/mindfulness.jpg",
                            Location = "Riverside Hotel, HCM",
                            MaxAttendees = 300,
                            OrganizerID = 2,
                            Price = 1900000L,
                            StartTime = new DateTime(2025, 10, 12, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Upcoming",
                            Title = "Mindfulness Seminar",
                            VenueID = 1
                        },
                        new
                        {
                            EventID = 3,
                            CategoryID = 4,
                            Description = "Latest AI research & enterprise applications.",
                            EndTime = new DateTime(2025, 11, 12, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "/img/events/ai-conf.jpg",
                            Location = "SECC, District 7",
                            MaxAttendees = 1500,
                            OrganizerID = 2,
                            Price = 1900000L,
                            StartTime = new DateTime(2025, 11, 10, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Upcoming",
                            Title = "AI Conference 2025",
                            VenueID = 3
                        },
                        new
                        {
                            EventID = 4,
                            CategoryID = 5,
                            Description = "5 km charity run for children's hospitals.",
                            EndTime = new DateTime(2025, 10, 5, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "/img/events/charity-run.jpg",
                            Location = "Thảo Cầm Viên, HCM",
                            MaxAttendees = 5000,
                            OrganizerID = 2,
                            Price = 1900000L,
                            StartTime = new DateTime(2025, 10, 5, 6, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Active",
                            Title = "Charity Run 2025",
                            VenueID = 1
                        },
                        new
                        {
                            EventID = 5,
                            CategoryID = 6,
                            Description = "Taste 100+ dishes from local vendors.",
                            EndTime = new DateTime(2025, 8, 22, 22, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "/img/events/food-fest.jpg",
                            Location = "September 23 Park",
                            MaxAttendees = 8000,
                            OrganizerID = 2,
                            Price = 1900000L,
                            StartTime = new DateTime(2025, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Active",
                            Title = "HCM Street Food Fest",
                            VenueID = 1
                        },
                        new
                        {
                            EventID = 6,
                            CategoryID = 7,
                            Description = "Showcase of contemporary Vietnamese artists.",
                            EndTime = new DateTime(2025, 7, 30, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "/img/events/art-expo.jpg",
                            Location = "Fine Arts Museum, HCM",
                            MaxAttendees = 300,
                            OrganizerID = 2,
                            Price = 1900000L,
                            StartTime = new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Active",
                            Title = "Modern Art Expo",
                            VenueID = 1
                        },
                        new
                        {
                            EventID = 7,
                            CategoryID = 8,
                            Description = "Pitch session for early‑stage startups.",
                            EndTime = new DateTime(2025, 9, 15, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "/img/events/demo-day.jpg",
                            Location = "Đà Nẵng Tech Park",
                            MaxAttendees = 200,
                            OrganizerID = 2,
                            Price = 1900000L,
                            StartTime = new DateTime(2025, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Upcoming",
                            Title = "Startup Demo Day",
                            VenueID = 2
                        },
                        new
                        {
                            EventID = 8,
                            CategoryID = 9,
                            Description = "Fund‑raising dinner with live auction.",
                            EndTime = new DateTime(2025, 12, 12, 22, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "/img/events/gala.jpg",
                            Location = "Saigon Opera House",
                            MaxAttendees = 400,
                            OrganizerID = 2,
                            Price = 1900000L,
                            StartTime = new DateTime(2025, 12, 12, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            Status = "Upcoming",
                            Title = "Gala Dinner for Hope",
                            VenueID = 1
                        },
                        new
                        {
                            EventID = 9,
                            CategoryID = 10,
                            Description = "Enjoy a classic under the stars.",
                            EndTime = new DateTime(2025, 6, 28, 22, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "/img/events/movie-night.jpg",
                            Location = "Crescent Lake Park, D7",
                            MaxAttendees = 1000,
                            OrganizerID = 2,
                            Price = 1900000L,
                            StartTime = new DateTime(2025, 6, 28, 19, 30, 0, 0, DateTimeKind.Unspecified),
                            Status = "Active",
                            Title = "Outdoor Movie Night – Classic Hits",
                            VenueID = 1
                        },
                        new
                        {
                            EventID = 10,
                            CategoryID = 2,
                            Description = "Learn to solve problems creatively.",
                            EndTime = new DateTime(2025, 8, 25, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            ImageUrl = "/img/events/design-thinking.jpg",
                            Location = "Indochina Riverside, Đà Nẵng",
                            MaxAttendees = 60,
                            OrganizerID = 2,
                            Price = 1900000L,
                            StartTime = new DateTime(2025, 8, 25, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Active",
                            Title = "Design Thinking Workshop",
                            VenueID = 2
                        });
                });

            modelBuilder.Entity("EventCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("EventCategories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            CategoryName = "Concert",
                            Description = "Live music concerts and shows"
                        },
                        new
                        {
                            CategoryID = 2,
                            CategoryName = "Workshop",
                            Description = "Hands-on training & skill-building sessions"
                        },
                        new
                        {
                            CategoryID = 3,
                            CategoryName = "Seminar",
                            Description = "Educational seminars and talks"
                        },
                        new
                        {
                            CategoryID = 4,
                            CategoryName = "Conference",
                            Description = "Large-scale professional conferences"
                        },
                        new
                        {
                            CategoryID = 5,
                            CategoryName = "Marathon",
                            Description = "Running & endurance sport events"
                        },
                        new
                        {
                            CategoryID = 6,
                            CategoryName = "Food Festival",
                            Description = "Culinary fairs and tasting events"
                        },
                        new
                        {
                            CategoryID = 7,
                            CategoryName = "Art Exhibition",
                            Description = "Galleries and art showcases"
                        },
                        new
                        {
                            CategoryID = 8,
                            CategoryName = "Startup Pitch",
                            Description = "Entrepreneurial pitch & demo days"
                        },
                        new
                        {
                            CategoryID = 9,
                            CategoryName = "Charity Event",
                            Description = "Fund-raising & community service"
                        },
                        new
                        {
                            CategoryID = 10,
                            CategoryName = "Movie Night",
                            Description = "Indoor / outdoor film screenings"
                        });
                });

            modelBuilder.Entity("EventController.Models.Entity.EmailVerificationToken", b =>
                {
                    b.Property<int>("TokenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("TokenID");

                    b.HasIndex("UserID");

                    b.ToTable("EmailVerificationTokens");
                });

            modelBuilder.Entity("EventController.Models.Entity.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Address = "123 Admin St, HCMC",
                            DateJoined = new DateTime(2025, 7, 9, 22, 55, 11, 882, DateTimeKind.Local).AddTicks(384),
                            DoB = new DateTime(1992, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "alice.admin@example.com",
                            FullName = "Alice Admin",
                            Gender = "Female",
                            IsEmailVerified = true,
                            Password = "P@ssw0rd!",
                            Phone = "0901234567",
                            ProfileImage = "/img/users/alice.jpg",
                            RoleID = 1,
                            Status = "Active"
                        },
                        new
                        {
                            UserID = 2,
                            Address = "456 Organizer Ave, Da Nang",
                            DateJoined = new DateTime(2025, 7, 9, 22, 55, 11, 882, DateTimeKind.Local).AddTicks(389),
                            DoB = new DateTime(1988, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "bob.organizer@example.com",
                            FullName = "Bob Organizer",
                            Gender = "Male",
                            IsEmailVerified = false,
                            Password = "P@ssw0rd!",
                            Phone = "0912345678",
                            ProfileImage = "/img/users/bob.jpg",
                            RoleID = 2,
                            Status = "Active"
                        },
                        new
                        {
                            UserID = 3,
                            Address = "789 Participant Rd, Hanoi",
                            DateJoined = new DateTime(2025, 7, 9, 22, 55, 11, 882, DateTimeKind.Local).AddTicks(392),
                            DoB = new DateTime(2000, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "charlie.participant@example.com",
                            FullName = "Charlie Participant",
                            Gender = "Other",
                            IsEmailVerified = true,
                            Password = "P@ssw0rd!",
                            Phone = "0923456789",
                            ProfileImage = "/img/users/charlie.jpg",
                            RoleID = 3,
                            Status = "Active"
                        });
                });

            modelBuilder.Entity("EventNote", b =>
                {
                    b.Property<int>("NoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.HasKey("NoteID");

                    b.HasIndex("EventID");

                    b.ToTable("EventNotes");
                });

            modelBuilder.Entity("Feedback", b =>
                {
                    b.Property<int>("FeedbackID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackID"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateSubmitted")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("FeedbackID");

                    b.HasIndex("EventID");

                    b.HasIndex("UserID");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationID"));

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SendAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("NotificationID");

                    b.HasIndex("EventID");

                    b.HasIndex("UserID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BankCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("InvoiceURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderInfo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("PaymentTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefundStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegistrationID")
                        .HasColumnType("int");

                    b.Property<string>("SecureHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TransactionCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PaymentID");

                    b.HasIndex("RegistrationID")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Registration", b =>
                {
                    b.Property<int>("RegistrationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegistrationID"));

                    b.Property<DateTime?>("CheckInTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CheckedIn")
                        .HasColumnType("bit");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("RegistrationID");

                    b.HasIndex("EventID");

                    b.HasIndex("UserID");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleID = 2,
                            RoleName = "Organizer"
                        },
                        new
                        {
                            RoleID = 3,
                            RoleName = "Participant"
                        });
                });

            modelBuilder.Entity("Venue", b =>
                {
                    b.Property<int>("VenueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VenueID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VenueID");

                    b.ToTable("Venues");

                    b.HasData(
                        new
                        {
                            VenueID = 1,
                            Address = "123 Lê Lợi, Quận 1, HCM",
                            Capacity = 2000,
                            Description = "Premium indoor concert hall in downtown Saigon.",
                            Image = "/img/venues/grand-hall.jpg",
                            Name = "Grand Hall"
                        },
                        new
                        {
                            VenueID = 2,
                            Address = "Hòa Khánh, Đà Nẵng",
                            Capacity = 500,
                            Description = "Modern coworking & workshop space for tech events.",
                            Image = "/img/venues/dn-techpark.jpg",
                            Name = "Đà Nẵng Tech Park"
                        },
                        new
                        {
                            VenueID = 3,
                            Address = "799 Nguyễn Văn Linh, Quận 7, HCM",
                            Capacity = 8000,
                            Description = "Largest expo hall in Ho Chi Minh City.",
                            Image = "/img/venues/secc.jpg",
                            Name = "SECC – Exhibition Center"
                        });
                });

            modelBuilder.Entity("Event", b =>
                {
                    b.HasOne("EventCategory", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventController.Models.Entity.User", "Organizer")
                        .WithMany("OrganizedEvents")
                        .HasForeignKey("OrganizerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Venue", "Venue")
                        .WithMany("Events")
                        .HasForeignKey("VenueID");

                    b.Navigation("Category");

                    b.Navigation("Organizer");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("EventController.Models.Entity.EmailVerificationToken", b =>
                {
                    b.HasOne("EventController.Models.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventController.Models.Entity.User", b =>
                {
                    b.HasOne("Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EventNote", b =>
                {
                    b.HasOne("Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Feedback", b =>
                {
                    b.HasOne("Event", "Event")
                        .WithMany("Feedbacks")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventController.Models.Entity.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Notification", b =>
                {
                    b.HasOne("Event", "Event")
                        .WithMany("Notifications")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventController.Models.Entity.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Payment", b =>
                {
                    b.HasOne("Registration", "Registration")
                        .WithOne("Payment")
                        .HasForeignKey("Payment", "RegistrationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Registration");
                });

            modelBuilder.Entity("Registration", b =>
                {
                    b.HasOne("Event", "Event")
                        .WithMany("Registrations")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventController.Models.Entity.User", "User")
                        .WithMany("Registrations")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Event", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Notifications");

                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("EventCategory", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("EventController.Models.Entity.User", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Notifications");

                    b.Navigation("OrganizedEvents");

                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("Registration", b =>
                {
                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Venue", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
