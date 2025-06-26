using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EventController.Models.Entity;

public class Event
{
    [Key]
    public int EventID { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    public string Location { get; set; }

    public int? VenueID { get; set; }

    [ForeignKey("VenueID")]
    public virtual Venue Venue { get; set; }

    [Required]
    public int OrganizerID { get; set; }

    [ForeignKey("OrganizerID")]
    public virtual User Organizer { get; set; }

    public int? MaxAttendees { get; set; }

    public string ImageUrl { get; set; }

    [Required]
    public int CategoryID { get; set; }

    [ForeignKey("CategoryID")]
    public virtual Category Category { get; set; }

    public string Status { get; set; }

    public bool IsPublic { get; set; } = true;

    public string Tags { get; set; }

    public string TimeZone { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; }

    public virtual ICollection<EventMedia> EventMedias { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; }
}
