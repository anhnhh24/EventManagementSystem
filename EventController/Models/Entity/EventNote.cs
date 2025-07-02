using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class EventNote
{
    [Key]
    public int NoteID { get; set; }

    public int EventID { get; set; }
    [ForeignKey("EventID")]
    public virtual Event Event { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}