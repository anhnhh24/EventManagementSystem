using EventController.Models.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using EventController.Models.Data.DBcontext; 
using EventController.Models.Entity;

namespace EventController.Models.DAO.Implements
{
    public class VenueDAO : IVenueDAO
    {
        private readonly DBContext _context;

        public VenueDAO(DBContext context)
        {
            _context = context;
        }

        public List<Venue> GetAllVenues()
        {
            return _context.Venues.ToList();
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
