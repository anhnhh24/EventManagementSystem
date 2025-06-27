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
        public DbSet<Category> Categories { get; set; }
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

            // 🔧 Tránh cascade delete gây conflict
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Event)
                .WithMany(e => e.Feedbacks)
                .HasForeignKey(f => f.EventID);

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
                .OnDelete(DeleteBehavior.Restrict); // Tuỳ chọn giữ cascade ở đây nếu bạn chắc

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Registration)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.RegistrationID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasData(

                new Category
                {
                    CategoryID = 1,
                    CategoryName = "Concert",
                    Description = "Live music concerts and shows",
                    Icon = "bi-music-note-beamed"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Workshop",
                    Description = "Hands-on training & skill-building sessions",
                    Icon = "bi-tools"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Seminar",
                    Description = "Educational seminars and talks",
                    Icon = "bi-mic"
                },
                new Category
                {
                    CategoryID = 4,
                    CategoryName = "Conference",
                    Description = "Large-scale professional conferences",
                    Icon = "bi-people"
                },
                new Category
                {
                    CategoryID = 5,
                    CategoryName = "Marathon",
                    Description = "Running & endurance sport events",
                    Icon = "bi-running"
                },
                new Category
                {
                    CategoryID = 6,
                    CategoryName = "Food Festival",
                    Description = "Culinary fairs and tasting events",
                    Icon = "bi-egg-fried"
                },
                new Category
                {
                    CategoryID = 7,
                    CategoryName = "Art Exhibition",
                    Description = "Galleries and art showcases",
                    Icon = "bi-brush"
                },
                new Category
                {
                    CategoryID = 8,
                    CategoryName = "Startup Pitch",
                    Description = "Entrepreneurial pitch & demo days",
                    Icon = "bi-lightbulb"
                },
                new Category
                {
                    CategoryID = 9,
                    CategoryName = "Charity Event",
                    Description = "Fund-raising & community service",
                    Icon = "bi-hand-heart"
                },
                new Category
                {
                    CategoryID = 10,
                    CategoryName = "Movie Night",
                    Description = "Indoor / outdoor film screenings",
                    Icon = "bi-film"
                }
            );
modelBuilder.Entity<Event>().HasData(
    new Event
    {
        EventID = 1,
        Title = "HCMC Live Music Night",
        Description = "Top V-Pop artists live on stage.",
        StartTime = new DateTime(2025, 9, 1, 18, 0, 0),
        EndTime = new DateTime(2025, 9, 1, 23, 0, 0),
        Location = "Grand Hall, District 1",
        VenueID = 1,
        OrganizerID = 2,      // make sure UserID = 2 exists
        MaxAttendees = 2000,
        ImageUrl = "/img/events/music-night.jpg",
        CategoryID = 1,      // Concert
        Status = "Active"
    },
    new Event
    {
        EventID = 2,
        Title = "Front-end Dev Bootcamp",
        Description = "Hands-on HTML / CSS / JS & React in 5 days.",
        StartTime = new DateTime(2025, 9, 18, 9, 0, 0),
        EndTime = new DateTime(2025, 9, 22, 17, 0, 0),
        Location = "Đà Nẵng Tech Park",
        VenueID = 2,
        OrganizerID = 2,
        MaxAttendees = 120,
        ImageUrl = "/img/events/frontend-bootcamp.jpg",
        CategoryID = 2,      // Workshop
        Status = "Active"
    },
    new Event
    {
        EventID = 3,
        Title = "AI Conference 2025",
        Description = "Latest AI research & enterprise applications.",
        StartTime = new DateTime(2025, 11, 10, 8, 0, 0),
        EndTime = new DateTime(2025, 11, 12, 18, 0, 0),
        Location = "SECC, District 7",
        VenueID = 3,
        OrganizerID = 2,
        MaxAttendees = 1500,
        ImageUrl = "/img/events/ai-conf.jpg",
        CategoryID = 4,      // Conference
        Status = "Upcoming"
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
