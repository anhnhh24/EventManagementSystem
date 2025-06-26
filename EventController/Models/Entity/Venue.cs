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

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string ContactInfo { get; set; }

    public string VenueType { get; set; }  // enum string or int
    public virtual ICollection<Event> Events { get; set; }
}