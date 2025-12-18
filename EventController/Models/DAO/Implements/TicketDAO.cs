using EventController.Models.Data.DBcontext;
using EventController.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventController.Models.DAO.Implements
{
    public class TicketDAO
    {
        private readonly DBContext _context;

        public TicketDAO(DBContext context)
        {
            _context = context;
        }

        // Generate a unique ticket code
        public string GenerateUniqueCode(int eventId)
        {
            string code;
            bool isUnique;

            do
            {
                // Format: EVT-{EventID}-{Random6Digits}
                string randomPart = new Random().Next(100000, 999999).ToString();
                code = $"EVT-{eventId}-{randomPart}";

                // Check if code already exists
                isUnique = !_context.Tickets.Any(t => t.UniqueCode == code);
            }
            while (!isUnique);

            return code;
        }

        // Create tickets after successful payment
        public void CreateTicketsForRegistration(int registrationId)
        {
            var registration = _context.Registrations
                .Include(r => r.Event)
                .Include(r => r.User)
                .FirstOrDefault(r => r.RegistrationID == registrationId);

            if (registration == null || registration.Status != "Success")
                return;

            // Create tickets based on quantity
            for (int i = 0; i < registration.Quantity; i++)
            {
                var ticket = new Ticket
                {
                    UserID = registration.UserID,
                    EventID = registration.EventID,
                    RegistrationID = registration.RegistrationID,
                    UniqueCode = GenerateUniqueCode(registration.EventID),
                    PurchaseDate = DateTime.Now,
                    Status = "Unused"
                };

                _context.Tickets.Add(ticket);
            }

            _context.SaveChanges();
        }

        // Get all tickets for a user
        public List<Ticket> GetUserTickets(int userId)
        {
            return _context.Tickets
                .Include(t => t.Event)
                    .ThenInclude(e => e.Venue)
                .Include(t => t.Event)
                    .ThenInclude(e => e.Category)
                .Include(t => t.Registration)
                .Where(t => t.UserID == userId)
                .OrderByDescending(t => t.PurchaseDate)
                .ToList();
        }

        // Get all tickets for a specific event
        public List<Ticket> GetTicketsByEvent(int eventId)
        {
            return _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Registration)
                .Where(t => t.EventID == eventId)
                .OrderBy(t => t.PurchaseDate)
                .ToList();
        }

        // Get ticket by unique code
        public Ticket GetTicketByCode(string uniqueCode)
        {
            return _context.Tickets
                .Include(t => t.Event)
                    .ThenInclude(e => e.Venue)
                .Include(t => t.User)
                .Include(t => t.Registration)
                .FirstOrDefault(t => t.UniqueCode == uniqueCode);
        }

        // Mark ticket as used
        public bool MarkTicketAsUsed(int ticketId)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.TicketID == ticketId);
            
            if (ticket == null || ticket.Status == "Used")
                return false;

            ticket.Status = "Used";
            _context.SaveChanges();
            return true;
        }

        // Cancel ticket
        public bool CancelTicket(int ticketId)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.TicketID == ticketId);
            
            if (ticket == null || ticket.Status != "Unused")
                return false;

            ticket.Status = "Cancelled";
            _context.SaveChanges();
            return true;
        }

        // Get ticket by ID
        public Ticket GetTicketById(int ticketId)
        {
            return _context.Tickets
                .Include(t => t.Event)
                    .ThenInclude(e => e.Venue)
                .Include(t => t.User)
                .Include(t => t.Registration)
                .FirstOrDefault(t => t.TicketID == ticketId);
        }

        // Cancel all tickets for an event (when admin cancels event)
        public void CancelAllEventTickets(int eventId)
        {
            var tickets = _context.Tickets
                .Where(t => t.EventID == eventId && t.Status == "Unused")
                .ToList();

            foreach (var ticket in tickets)
            {
                ticket.Status = "Cancelled";
            }

            if (tickets.Any())
            {
                _context.SaveChanges();
            }
        }
    }
}
