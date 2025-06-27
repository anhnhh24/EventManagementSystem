using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventController.Models.Entity
{
    public class EmailVerificationToken
    {
        [Key]
        public int TokenID { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiresAt { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}
