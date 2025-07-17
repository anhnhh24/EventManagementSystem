using EventController.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Registration
{
    [Key]
    public int RegistrationID { get; set; }

    [Required]
    public int UserID { get; set; }
    public virtual User User { get; set; }

    [Required]
    public int EventID { get; set; }
    public virtual Event Event { get; set; }

    public DateTime RegisterDate { get; set; } = DateTime.Now;

    public string Status { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    public int Quantity { get; set; }

    public int? BillID { get; set; }
    public virtual Bill Bill { get; set; }
}
