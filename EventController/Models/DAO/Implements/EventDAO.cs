using EventController.Models.Data.DBcontext;
using Microsoft.EntityFrameworkCore;

namespace EventController.Models.DAO.Implements
{
    public class EventDAO
    {
        private readonly DBContext _context;

        public EventDAO(DBContext context)
        {
            _context = context;
        }

        public List<Event> GetAllEvents()
        {
            return _context.Events
                           .Include(e => e.Category)
                           .Include(e => e.Organizer)
                           .OrderBy(e => e.StartTime)
                           .ToList()
                          ;
        }

        public Event GetEventById(int id)
        {
            return _context.Events
                           .Include(e => e.Category)
                           .Include(e => e.Organizer)
                           .Include(e => e.Venue)
                           .FirstOrDefault(e => e.EventID == id);
        }
        public void AddEvent(Event evt)
        {
            _context.Events.Add(evt);
            _context.SaveChanges();
        }

        public void UpdateEvent(Event evt)
        {
            _context.Events.Update(evt);
            _context.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            var evt = _context.Events.Find(id);
            if (evt != null)
            {
                _context.Events.Remove(evt);
                _context.SaveChanges();
            }
        }
        public bool EventExists(int id) => _context.Events.Any(e => e.EventID == id);

        public List<Event> SearchEventsByTitle(string keyword)
        {
            return _context.Events
                           .Where(e => e.Title.Contains(keyword))
                           .ToList();
        }

        public List<Event> GetEventsByCategory(int categoryId)
        {
            return _context.Events
                           .Where(e => e.CategoryID == categoryId)
                           .ToList();
        }

        public List<Event> GetEventsByOrganizer(int organizerId)
        {
            return _context.Events
                           .Where(e => e.OrganizerID == organizerId)
                           .ToList();
        }
        public List<Event> GetUpcomingEvents(DateTime? from = null)
        {
            DateTime start = from ?? DateTime.UtcNow;
            return _context.Events
                           .Where(e => e.StartTime >= start)
                           .OrderBy(e => e.StartTime)
                           .ToList();
        }

        public List<Event> GetPastEvents(DateTime? until = null)
        {
            DateTime end = until ?? DateTime.UtcNow;
            return _context.Events
                           .Where(e => e.EndTime < end)
                           .OrderByDescending(e => e.EndTime)
                           .ToList();
        }

        public List<Event> GetEventsWithAvailableSeats()
        {
            return _context.Events
                           .Include(e => e.Registrations)          // cần để đếm
                           .Where(e => e.MaxAttendees == null ||
                                       e.Registrations.Count() < e.MaxAttendees)
                           .ToList();
        }
    }
}
