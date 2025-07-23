using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

public class Venue
{
    [Key]
    public int VenueID { get; set; }

    [Required]
    public string Name { get; set; }

    public string Address { get; set; }

    public int? Capacity { get; set; }

    public string Description { get; set; }

    public string Image { get; set; } 

    public virtual ICollection<Event> Events { get; set; }
}