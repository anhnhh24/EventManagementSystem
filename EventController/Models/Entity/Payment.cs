using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Payment
{
    [Key]
    public int PaymentID { get; set; }

    [Required]
    public int RegistrationID { get; set; }

    [ForeignKey("RegistrationID")]
    public virtual Registration Registration { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public DateTime? PaymentTime { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } 

    [MaxLength(100)]
    public string TransactionCode { get; set; }  

    [MaxLength(100)]
    public string OrderInfo { get; set; }       

    [MaxLength(100)]
    public string BankCode { get; set; }        

    [MaxLength(100)]
    public string CardType { get; set; }       

    [MaxLength(100)]
    public string SecureHash { get; set; }      

    public string RefundStatus { get; set; }     

    public string InvoiceURL { get; set; }

    public string Note { get; set; }
}
