using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class EventMedia
{
    [Key]
    public int MediaID { get; set; }

    [Required]
    public int EventID { get; set; }

    [ForeignKey("EventID")]
    public virtual Event Event { get; set; }

    public string MediaUrl { get; set; }

    public string Type { get; set; }

    public DateTime UploadDate { get; set; } = DateTime.Now;
}