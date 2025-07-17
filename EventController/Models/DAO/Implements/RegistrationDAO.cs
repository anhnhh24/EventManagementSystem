using EventController.Models.Data.DBcontext;
using EventController.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventController.Models.DAO.Implements
{
    public class RegistrationDAO
    {
        private readonly DBContext _context;
        EventDAO _eventDAO;
        public RegistrationDAO(DBContext context, EventDAO eventDAO)
        {
            _context = context;
            _eventDAO = eventDAO;
        }

        public bool IsUserRegistered(int userId, int eventId)
        {
            return _context.Registrations.Any(r => r.UserID == userId && r.EventID == eventId);
        }

        public bool RegisterUserToEvent(int userId, int eventId)
        { 
            var registration = new Registration
            {
                UserID = userId,
                EventID = eventId,
                RegisterDate = DateTime.Now,
                Status = "Pending",
                Quantity = 1,
                Total = _eventDAO.GetEventById(eventId).Price
            };

            _context.Registrations.Add(registration);
            _context.SaveChanges();

            return true;
        }

        public bool CancelRegistration(int Id)
        {
            var reg = _context.Registrations
                .FirstOrDefault(r => r.RegistrationID == Id);

            if (reg == null) return false;

            _context.Registrations.Remove(reg);
            _context.SaveChanges();
            return true;
        }

        public List<Event> GetRegisteredEvents(int userId)
        {
            return _context.Registrations
                .Where(r => r.UserID == userId)
                .Include(r => r.Event)
                .Select(r => r.Event)
                .ToList();
        }

        public void UpdateStatusById(int Id, string status)
        {
            var itemToUpdate = _context.Registrations
                                        .FirstOrDefault(r => r.RegistrationID == Id);
            itemToUpdate.Status = status;
            _context.Registrations.Update(itemToUpdate);
            _context.SaveChanges();
        }

        public List<Registration> getPendingUserRegistration(int userId)
        {
            return _context.Registrations.
                Where(r => r.UserID == userId && r.Status == "Pending").
                Include(r => r.Event).
                ToList();
        }
        public void Update(Registration registration)
        {
            _context.Registrations.Update(registration);
            _context.SaveChanges();
        }

        public Registration GetById(int Id)
        {
            return _context.Registrations.Include(r => r.Event).FirstOrDefault(r => r.RegistrationID == Id);
        }

        public bool IsValidEventAttendees(int eventId, int quantity)
        {
            var evt = _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefault(e => e.EventID == eventId);

            if (evt.MaxAttendees != null &&
                (evt.CurrentAttendees + quantity > evt.MaxAttendees))
                return false;

            return true;
        }
    }
}
