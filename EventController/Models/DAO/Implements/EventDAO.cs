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
                           .ToList();
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
                           .Where(e => e.Title.Contains(keyword) && e.StartTime >= DateTime.UtcNow)
                           .ToList();
        }

        public List<Event> GetEventsByCategory(int categoryId)
        {
            return _context.Events
                           .Where(e => e.CategoryID == categoryId && e.StartTime >= DateTime.UtcNow)
                           .ToList();
        }

        public List<Event> GetEventsByOrganizer(int organizerId)
        {
            return _context.Events
                           .Where(e => e.OrganizerID == organizerId && e.StartTime >= DateTime.UtcNow)
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
                           .Include(e => e.Registrations)
                           .Where(e => (e.MaxAttendees == null || e.Registrations.Count() < e.MaxAttendees)
                                       && e.StartTime >= DateTime.UtcNow)
                           .ToList();
        }

        public IQueryable<Event> GetQueryableEvents()
        {
            return _context.Events
                           .Include(e => e.Category)
                           .Include(e => e.Venue)
                           .Where(e => e.StartTime >= DateTime.UtcNow);
        }

        public bool IsVenueOccupied(int venueId, DateTime start, DateTime end)
        {
            return _context.Events.Any(e =>
                e.VenueID == venueId &&
                e.Status == "Active" &&
                (
                    (start >= e.StartTime && start < e.EndTime) ||
                    (end > e.StartTime && end <= e.EndTime) ||
                    (start <= e.StartTime && end >= e.EndTime)
                )
            );
        }

        public void UpdateEventStatuses()
        {
            var now = DateTime.UtcNow;

            var expiredEvents = _context.Events
                .Where(e => e.EndTime < now && e.Status != "Expired")
                .ToList();

            foreach (var evt in expiredEvents)
            {
                evt.Status = "Expired";
            }

            var activeEvents = _context.Events
                .Where(e => e.StartTime <= now && e.EndTime > now && e.Status != "Active")
                .ToList();

            foreach (var evt in activeEvents)
            {
                evt.Status = "Active";
            }

            if (expiredEvents.Any() || activeEvents.Any())
            {
                _context.SaveChanges();
            }
        }

        public bool CreateEvent(Event evt)
        {
            try
            {
                evt.Status = "Inactive";
                evt.CurrentAttendees = 0;
                _context.Events.Add(evt);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool IncreaseEventAttendees(int eventId, int quantity)
        {
            try
            {
                var evt = _context.Events.Find(eventId);
                evt.CurrentAttendees += quantity;
                _context.Events.Update(evt);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
