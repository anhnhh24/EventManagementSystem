using EventController.Models.Data.DBcontext;
using EventController.Models.Entity;
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

        public List<Event> GetAllEventsThisMonth()
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfNextMonth = startOfMonth.AddMonths(2);

            return _context.Events
                           .Include(e => e.Category)
                           .Include(e => e.Organizer)
                           .Where(e => e.StartTime >= startOfMonth && e.StartTime < startOfNextMonth 
                                    && (e.Status == "Active" || e.Status == "Upcoming"))
                           .OrderBy(e => e.StartTime)
                           .ToList();
        }

        public List<Event> GetAllExpiredEvent()
        {
            var now = DateTime.Now;
            return _context.Events
                           .Include(e => e.Category)
                           .Include(e => e.Organizer)
                           .Where(e => e.Status == "Expired")
                           .OrderByDescending(e => e.StartTime)
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
                           .Where(e => e.OrganizerID == organizerId)
                           .Include(e => e.Venue)
                           .Include(e => e.Category)
                           .OrderByDescending(e => e.CreatedAt)
                           .ToList();
        }
        public List<Event> GetEventsInNextWeek()
        {
            var today = DateTime.UtcNow;
            var nextWeek = today.AddDays(7);

            return _context.Events
                           .Where(e => e.StartTime >= today && e.StartTime <= nextWeek && e.Status != "Inactive")
                           .OrderBy(e => e.StartTime)
                           .ToList();
        }


        public List<Event> GetUpcomingEvents(DateTime? from = null)
        {
            DateTime start = from ?? DateTime.UtcNow;
            return _context.Events
                           .Where(e => e.StartTime >= start && (e.Status == "Upcoming" || e.Status == "Active"))
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
                                       && e.StartTime >= DateTime.UtcNow
                                       && e.Status != "Inactive")
                           .ToList();
        }



        // Check if venue has conflicting events in the given time range
        public bool HasVenueConflict(int? venueId, DateTime startTime, DateTime endTime, int? excludeEventId = null)
        {
            if (!venueId.HasValue)
                return false;

            var conflictingEvents = _context.Events
                .Where(e => e.VenueID == venueId.Value
                    && (e.Status == "Active" || e.Status == "Upcoming")
                    && e.EventID != (excludeEventId ?? 0)
                    && ((e.StartTime < endTime && e.EndTime > startTime)))
                .ToList();

            return conflictingEvents.Any();
        }

        // Get venue's active and upcoming events
        public List<Event> GetVenueActiveUpcomingEvents(int venueId)
        {
            return _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .Where(e => e.VenueID == venueId 
                    && (e.Status == "Active" || e.Status == "Upcoming"))
                .OrderBy(e => e.StartTime)
                .ToList();
        }

        public IQueryable<Event> GetQueryableEvents()
        {
            return _context.Events
                           .Include(e => e.Category)
                           .Include(e => e.Venue)
                           .Where(e => e.Status != "Expired" && e.Status != "Cancelled" && e.Status != "Inactive");
        }

        public bool IsVenueOccupied(int venueId, DateTime start, DateTime end, int currentEventId)
        {
            return _context.Events.Any(e =>
                e.EventID != currentEventId &&
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
            var now = DateTime.Now; // Changed from UtcNow to Now for local time

            // Update expired events
            var expiredEvents = _context.Events
                .Where(e => e.EndTime < now && e.Status != "Expired")
                .ToList();

            foreach (var evt in expiredEvents)
            {
                evt.Status = "Expired";
            }

            // Update active (ongoing) events - only if they're currently Upcoming
            var activeEvents = _context.Events
                .Where(e => e.StartTime <= now && e.EndTime > now && e.Status == "Upcoming")
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
