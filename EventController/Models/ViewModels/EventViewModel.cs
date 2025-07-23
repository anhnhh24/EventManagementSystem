using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class EventViewModel
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessage = "Start time is required.")]
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "End time is required.")]
    [DataType(DataType.DateTime)]
    public DateTime EndTime { get; set; }

    [Display(Name = "Venue")]
    public int? VenueID { get; set; }

    [Required]
    public int OrganizerID { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    [Display(Name = "Category")]
    public int CategoryID { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
    [Display(Name = "Max Attendees")]
    public int? MaxAttendees { get; set; }

    public string? Status { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "Price must be 0 or greater.")]
    public long Price { get; set; }
    public IFormFile? EventImage { get; set; }
}
