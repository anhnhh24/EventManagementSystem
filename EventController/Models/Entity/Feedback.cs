using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EventController.Models.Entity;

public class Feedback
{
    [Key]
    public int FeedbackID { get; set; }

    [Required]
    public int UserID { get; set; }

    [ForeignKey("UserID")]
    public virtual User User { get; set; }

    [Required]
    public int EventID { get; set; }

    [ForeignKey("EventID")]
    public virtual Event Event { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public string Comment { get; set; }

    public DateTime DateSubmitted { get; set; } = DateTime.Now;
}
