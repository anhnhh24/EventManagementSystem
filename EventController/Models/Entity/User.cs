using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EventController.Models.Entity
{
        public class User
        {
            [Key]
            public int UserID { get; set; }

            [Required, MaxLength(100)]
            public string FullName { get; set; }

            [Required, MaxLength(100)]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

            public string PasswordSalt { get; set; }

            [MaxLength(20)]
            public string Phone { get; set; }

            [Required]
            public int RoleID { get; set; }

            [ForeignKey("RoleID")]
            public virtual Role Role { get; set; }

            public string ProfileImage { get; set; }

            public string Status { get; set; }  // could be enum or string

            public bool IsEmailVerified { get; set; }

            public DateTime DateJoined { get; set; } = DateTime.Now;

            public virtual ICollection<Event> OrganizedEvents { get; set; }
            public virtual ICollection<Registration> Registrations { get; set; }
            public virtual ICollection<Feedback> Feedbacks { get; set; }
            public virtual ICollection<Notification> Notifications { get; set; }
    }
}
