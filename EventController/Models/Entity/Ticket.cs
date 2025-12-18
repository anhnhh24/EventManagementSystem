using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventController.Models.Entity
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }

        [Required]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int EventID { get; set; }
        public virtual Event Event { get; set; }

        [Required]
        public int RegistrationID { get; set; }
        public virtual Registration Registration { get; set; }

        [Required]
        [StringLength(100)]
        public string UniqueCode { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Unused"; // Unused, Used, Cancelled
    }
}
