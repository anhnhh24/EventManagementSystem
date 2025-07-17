using EventController.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Bill
{
    [Key]
    public int BillID { get; set; }

    [Required]
    public int UserID { get; set; }

    public virtual User User { get; set; }

    [Required]
    [Range(0, long.MaxValue)]
    public long TotalAmount { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<Registration> Registrations { get; set; }

    public virtual Payment Payment { get; set; }

}
