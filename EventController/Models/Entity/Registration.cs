using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EventController.Models.Entity;

public class Registration
{
    [Key]
    public int RegistrationID { get; set; }

    [Required]
    public int UserID { get; set; }

    [ForeignKey("UserID")]
    public virtual User User { get; set; }

    [Required]
    public int EventID { get; set; }

    [ForeignKey("EventID")]
    public virtual Event Event { get; set; }

    public DateTime RegisterDate { get; set; } = DateTime.Now;

    public string Status { get; set; }

    public bool CheckedIn { get; set; }

    public DateTime? CheckInTime { get; set; }

    public virtual Payment Payment { get; set; }
}