using EventController.Models.Data.DBcontext;
using Microsoft.EntityFrameworkCore;

namespace EventController.Models.DAO.Implements
{
    public class BillDAO
    {
        private readonly DBContext _context;

        public BillDAO(DBContext context)
        {
            _context = context;
        }
        public async Task<Bill> CreateBillAsync(int userId, List<int> registrationIds, long totalAmount)
        {
            var bill = new Bill
            {
                UserID = userId,
                TotalAmount = totalAmount,
                Status = "Pending"
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            var regs = _context.Registrations.Where(r => registrationIds.Contains(r.RegistrationID));
            foreach (var reg in regs)
            {
                reg.BillID = bill.BillID;
            }

            await _context.SaveChangesAsync();
            return bill;
        }
        public List<Registration> GetRegistrationByBillId(int billId)
        {
            return _context.Bills
                .Where(b => b.BillID == billId)
                .Include(b => b.Registrations)
                .SelectMany(b => b.Registrations)
                .ToList();
        }
        public void UpdateBill(Bill Bill)
        {
            _context.Update(Bill);
            _context.SaveChanges();
        }
        public Bill GetBillByBillId(int billId)
        {
            return _context.Bills
                .Include(b => b.Registrations)
                .Include(b => b.Payment)
                .FirstOrDefault(b => b.BillID == billId);
        }

        public List<Bill> GetBillsByUserId(int userId)
        {
            return _context.Bills
                .Include(b => b.Registrations)
                    .ThenInclude(r => r.Event) 
                .Include(b => b.Payment)
                .Include(b => b.User) 
                .Where(b => b.UserID == userId && b.Status == "Paid")
                .OrderByDescending(b => b.CreatedAt)
                .ToList();
        }
    }
}
