using EventController.Models.Data.DBcontext;
using EventController.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventController.Models.DAO.Implements
{
    public class PaymentDAO
    {
        private readonly DBContext _context;

        public PaymentDAO(DBContext context)
        {
            _context = context;
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        public List<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public Payment GetPaymentById(int id)
        {
            return _context.Payments
                .Include(p => p.Bill)
                .ThenInclude(r => r.User)
                .FirstOrDefault(p => p.PaymentID == id);
        }
        public Payment GetPaymentByTransactionCode(string trannsactionCode)
        {
            return _context.Payments
                .Include(p => p.Bill)
                .ThenInclude(r => r.User)
                .FirstOrDefault(p => p.TransactionCode == trannsactionCode);
        }

        public void DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }
    }
}
