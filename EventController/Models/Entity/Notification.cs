using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EventController.Models.Entity;

public class Notification
{
    [Key]
    public int NotificationID { get; set; }

    [Required]
    public int UserID { get; set; }

    [ForeignKey("UserID")]
    public virtual User User { get; set; }

    public int? EventID { get; set; }

    [ForeignKey("EventID")]
    public virtual Event Event { get; set; }

    public string Content { get; set; }

    public DateTime SentTime { get; set; } = DateTime.Now;

    public string NotificationType { get; set; }

    public bool IsRead { get; set; }
}