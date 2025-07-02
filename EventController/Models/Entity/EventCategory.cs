using System.ComponentModel.DataAnnotations;

public class EventCategory
{
    [Key]
    public int CategoryID { get; set; }

    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Event> Events { get; set; }
}