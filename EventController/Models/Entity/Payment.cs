using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Payment
{
    [Key]
    public int PaymentID { get; set; }

    [Required]
    public int BillID { get; set; }
    [ForeignKey("BillID")]
    public virtual Bill Bill { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public string? TransactionCode { get; set; }
    public string? OrderInfo { get; set; }
    public DateTime? PaymentTime { get; set; }

    public DateTime? ExpireTime { get; set; } 

    [MaxLength(20)]
    public string Status { get; set; } = "Pending";
}