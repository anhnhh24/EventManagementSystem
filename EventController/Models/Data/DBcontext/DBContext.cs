using EventController.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventController.Models.Data.DBcontext
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Admin" },
                new Role { RoleID = 2, RoleName = "Organizer" },
                new Role { RoleID = 3, RoleName = "Participant" });

            modelBuilder.Entity<Notification>()
      .HasOne(n => n.User)
      .WithMany(u => u.Notifications)
      .HasForeignKey(n => n.UserID)
      .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Event)
                .WithMany(e => e.Notifications)
                .HasForeignKey(n => n.EventID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.User)
                .WithMany(u => u.Registrations)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Bill)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BillID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<EventCategory>().HasData(

                new EventCategory
                {
                    CategoryID = 1,
                    CategoryName = "Concert",
                    Description = "Live music concerts and shows"
                },
                new EventCategory
                {
                    CategoryID = 2,
                    CategoryName = "Workshop",
                    Description = "Hands-on training & skill-building sessions"
                },
                new EventCategory
                {
                    CategoryID = 3,
                    CategoryName = "Seminar",
                    Description = "Educational seminars and talks"
                },
                new EventCategory
                {
                    CategoryID = 4,
                    CategoryName = "Conference",
                    Description = "Large-scale professional conferences"
                },
                new EventCategory
                {
                    CategoryID = 5,
                    CategoryName = "Marathon",
                    Description = "Running & endurance sport events"
                },
                new EventCategory
                {
                    CategoryID = 6,
                    CategoryName = "Food Festival",
                    Description = "Culinary fairs and tasting events"
                },
                new EventCategory
                {
                    CategoryID = 7,
                    CategoryName = "Art Exhibition",
                    Description = "Galleries and art showcases"
                },
                new EventCategory
                {
                    CategoryID = 8,
                    CategoryName = "Startup Pitch",
                    Description = "Entrepreneurial pitch & demo days"
                },
                new EventCategory
                {
                    CategoryID = 9,
                    CategoryName = "Charity Event",
                    Description = "Fund-raising & community service"
                },
                new EventCategory
                {
                    CategoryID = 10,
                    CategoryName = "Movie Night",
                    Description = "Indoor / outdoor film screenings"
                }
            );
            modelBuilder.Entity<Event>().HasData(
            new Event
            {
                EventID = 2,
                Title = "Mindfulness Seminar",
                Description = "An evening talk on mindfulness & wellbeing.",
                StartTime = new DateTime(2025, 10, 12, 19, 0, 0),
                EndTime = new DateTime(2025, 10, 12, 21, 0, 0),
                VenueID = 1,
                Price = 1900000,
                OrganizerID = 2,
                MaxAttendees = 300,
                ImageUrl = "/img/events/mindfulness.jpg",
                CategoryID = 3,
                Status = "Upcoming"
            },
            new Event
            {
                EventID = 3,
                Title = "AI Conference 2025",
                Description = "Latest AI research & enterprise applications.",
                StartTime = new DateTime(2025, 11, 10, 8, 0, 0),
                EndTime = new DateTime(2025, 11, 12, 18, 0, 0),
                VenueID = 3,
                OrganizerID = 2,
                Price = 1900000,
                MaxAttendees = 1500,
                ImageUrl = "/img/events/ai-conf.jpg",
                CategoryID = 4,
                Status = "Upcoming"
            },
            new Event
            {
                EventID = 4,
                Title = "Charity Run 2025",
                Description = "5 km charity run for children's hospitals.",
                StartTime = new DateTime(2025, 10, 5, 6, 0, 0),
                EndTime = new DateTime(2025, 10, 5, 11, 0, 0),
                VenueID = 1,
                Price = 1900000,
                OrganizerID = 2,
                MaxAttendees = 5000,
                ImageUrl = "/img/events/charity-run.jpg",
                CategoryID = 5,
                Status = "Active"
            },
            new Event
            {
                EventID = 5,
                Title = "HCM Street Food Fest",
                Description = "Taste 100+ dishes from local vendors.",
                StartTime = new DateTime(2025, 8, 20, 10, 0, 0),
                EndTime = new DateTime(2025, 8, 22, 22, 0, 0),
                VenueID = 1,
                OrganizerID = 2,
                Price = 1900000,
                MaxAttendees = 8000,
                ImageUrl = "/img/events/food-fest.jpg",
                CategoryID = 6,
                Status = "Active"
            },
            new Event
            {
                EventID = 6,
                Title = "Modern Art Expo",
                Description = "Showcase of contemporary Vietnamese artists.",
                StartTime = new DateTime(2025, 7, 1, 10, 0, 0),
                EndTime = new DateTime(2025, 7, 30, 20, 0, 0),
                VenueID = 1,
                OrganizerID = 2,
                MaxAttendees = 300,
                Price = 1900000,
                ImageUrl = "/img/events/art-expo.jpg",
                CategoryID = 7,
                Status = "Active"
            },
            new Event
            {
                EventID = 7,
                Title = "Startup Demo Day",
                Description = "Pitch session for early‑stage startups.",
                StartTime = new DateTime(2025, 9, 15, 14, 0, 0),
                EndTime = new DateTime(2025, 9, 15, 18, 0, 0),
                VenueID = 2,
                OrganizerID = 2,
                MaxAttendees = 200,
                Price = 1900000,
                ImageUrl = "/img/events/demo-day.jpg",
                CategoryID = 8,
                Status = "Upcoming"
            },
            new Event
            {
                EventID = 8,
                Title = "Gala Dinner for Hope",
                Description = "Fund‑raising dinner with live auction.",
                StartTime = new DateTime(2025, 12, 12, 18, 30, 0),
                EndTime = new DateTime(2025, 12, 12, 22, 0, 0),
                VenueID = 1,
                OrganizerID = 2,
                Price = 1900000,
                MaxAttendees = 400,
                ImageUrl = "/img/events/gala.jpg",
                CategoryID = 9,
                Status = "Upcoming"
            },
            new Event
            {
                EventID = 9,
                Title = "Outdoor Movie Night – Classic Hits",
                Description = "Enjoy a classic under the stars.",
                StartTime = new DateTime(2025, 6, 28, 19, 30, 0),
                EndTime = new DateTime(2025, 6, 28, 22, 0, 0),
                VenueID = 1,
                OrganizerID = 2,
                MaxAttendees = 1000,
                Price = 1900000,
                ImageUrl = "/img/events/movie-night.jpg",
                CategoryID = 10,
                Status = "Active"
            },
            new Event
            {
                EventID = 10,
                Title = "Design Thinking Workshop",
                Description = "Learn to solve problems creatively.",
                StartTime = new DateTime(2025, 8, 25, 9, 0, 0),
                EndTime = new DateTime(2025, 8, 25, 17, 0, 0),
                VenueID = 2,
                OrganizerID = 2,
                MaxAttendees = 60,
                Price = 1900000,
                ImageUrl = "/img/events/design-thinking.jpg",
                CategoryID = 2,
                Status = "Active"
            }
            );


            modelBuilder.Entity<Venue>().HasData(
       new Venue
       {
           VenueID = 1,
           Name = "Grand Hall",
           Address = "123 Lê Lợi, Quận 1, HCM",
           Capacity = 2000,
           Description = "Premium indoor concert hall in downtown Saigon.",
           Image = "/img/venues/grand-hall.jpg"
       },
       new Venue
       {
           VenueID = 2,
           Name = "Đà Nẵng Tech Park",
           Address = "Hòa Khánh, Đà Nẵng",
           Capacity = 500,
           Description = "Modern coworking & workshop space for tech events.",
           Image = "/img/venues/dn-techpark.jpg"
       },
       new Venue
       {
           VenueID = 3,
           Name = "SECC – Exhibition Center",
           Address = "799 Nguyễn Văn Linh, Quận 7, HCM",
           Capacity = 8000,
           Description = "Largest expo hall in Ho Chi Minh City.",
           Image = "/img/venues/secc.jpg"
       }
   );

            modelBuilder.Entity<User>().HasData(
       new User
       {
           UserID = 1,
           FullName = "Alice Admin",
           Email = "alice.admin@example.com",
           Password = "P@ssw0rd!",          // ⚠ replace with hashed value in production
           Phone = "0901234567",
           RoleID = 1,                    // Admin
           Address = "123 Admin St, HCMC",
           ProfileImage = "/img/users/alice.jpg",
           Status = "Active",
           IsEmailVerified = true,
           DateJoined = DateTime.Now,
           Gender = "Female",
           DoB = new DateTime(1992, 3, 15)
       },
       new User
       {
           UserID = 2,
           FullName = "Bob Organizer",
           Email = "bob.organizer@example.com",
           Password = "P@ssw0rd!",
           Phone = "0912345678",
           RoleID = 2,                    // Organizer
           Address = "456 Organizer Ave, Da Nang",
           ProfileImage = "/img/users/bob.jpg",
           Status = "Active",
           IsEmailVerified = false,
           DateJoined = DateTime.Now,
           Gender = "Male",
           DoB = new DateTime(1988, 7, 21)
       },
       new User
       {
           UserID = 3,
           FullName = "Charlie Participant",
           Email = "charlie.participant@example.com",
           Password = "P@ssw0rd!",
           Phone = "0923456789",
           RoleID = 3,                    // Participant
           Address = "789 Participant Rd, Hanoi",
           ProfileImage = "/img/users/charlie.jpg",
           Status = "Active",
           IsEmailVerified = true,
           DateJoined = DateTime.Now,
           Gender = "Other",
           DoB = new DateTime(2000, 11, 5)
       }
   );


        }

    }

}
