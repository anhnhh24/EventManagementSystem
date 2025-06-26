using EventController.Models.Entity;
using System;
using System.Collections.Generic;

namespace EventController.Models.DAO.Interfaces
{
    public interface IEventDAO
    {
        public List<Event> GetAllEvents();
        public Event GetEventById(int id);
        public void AddEvent(Event evt);
        public void UpdateEvent(Event evt);
        public void DeleteEvent(int id);
        public bool EventExists(int id);
        public List<Event> SearchEventsByTitle(string keyword);
        public List<Event> GetEventsByCategory(int categoryId);
        public List<Event> GetEventsByOrganizer(int organizerId);
        public List<Event> GetUpcomingEvents(DateTime? from = null);
        public List<Event> GetPastEvents(DateTime? until = null);
        public List<Event> GetEventsWithAvailableSeats();
    }
}
