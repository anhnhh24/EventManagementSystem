using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int CategoryID { get; set; }

    [Required, MaxLength(100)]
    public string CategoryName { get; set; }
    public string Description { get; set; }

    public string Icon { get; set; }

    public virtual ICollection<Event> Events { get; set; }
}