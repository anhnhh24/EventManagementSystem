using EventController.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Notification
{
    [Key]
    public int NotificationID { get; set; }

    public int UserID { get; set; }
    [ForeignKey("UserID")]
    public virtual User User { get; set; }

    public int EventID { get; set; }
    [ForeignKey("EventID")]
    public virtual Event Event { get; set; }

    [Required]
    public string Message { get; set; }

    public DateTime SendAt { get; set; }
    public bool IsSent { get; set; } = false;
}