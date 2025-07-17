public class BillDetailViewModel
{
    public int BillID { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public long TotalAmount { get; set; }

    public List<RegistrationInfo> Registrations { get; set; }
}

public class RegistrationInfo
{
    public string EventTitle { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
}