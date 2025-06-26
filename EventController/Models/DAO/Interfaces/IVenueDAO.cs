namespace EventController.Models.DAO.Interfaces
{
    public interface IVenueDAO
    {
        public List<Venue> GetAllVenues();
        public Venue GetVenueById(int id);
        public void AddVenue(Venue venue);
        public void UpdateVenue(Venue venue);
        public void DeleteVenue(int id);
    }
}
