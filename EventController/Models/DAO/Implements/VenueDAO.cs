using Microsoft.EntityFrameworkCore;
using EventController.Models.Data.DBcontext; 
using EventController.Models.Entity;

namespace EventController.Models.DAO.Implements
{
    public class VenueDAO
    {
        private readonly DBContext _context;

        public VenueDAO(DBContext context)
        {
            _context = context;
        }

        public List<Venue> GetAllVenues()
        {
            return _context.Venues
                .Include(v => v.Events)
                .ToList();
        }

        public Venue GetVenueById(int id)
        {
            return _context.Venues
                .Include(v => v.Events) // optional: include related events
                .FirstOrDefault(v => v.VenueID == id);
        }

        public void AddVenue(Venue venue)
        {
            _context.Venues.Add(venue);
            _context.SaveChanges();
        }

        public void UpdateVenue(Venue venue)
        {
            var existingVenue = _context.Venues.Local.FirstOrDefault(v => v.VenueID == venue.VenueID);
            if (existingVenue != null)
            {
                _context.Entry(existingVenue).State = EntityState.Detached;
            }
            _context.Venues.Update(venue);
            _context.SaveChanges();
        }

        public void DeleteVenue(int id)
        {
            var venue = _context.Venues.Find(id);
            if (venue != null)
            {
                _context.Venues.Remove(venue);
                _context.SaveChanges();
            }
        }
    }
}
