using EventController.Models.DAO.Implements;
using Microsoft.AspNetCore.Mvc;

namespace EventController.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        EventDAO _eventDAO;
        public EventController(ILogger<EventController> logger, EventDAO eventDAO)
        {
            _logger = logger;
            _eventDAO = eventDAO;
        }

        public IActionResult Index(int id)
        {
            if (id != null)
            {
                var evt = _eventDAO.GetEventById(id);
                if (evt == null) return RedirectToAction("Index", "Home");
                ViewBag.Event = evt;
            }
            return View();
        }
        public IActionResult EventList(int page, int pageSize)
        {
            var events = _eventDAO.GetAllEvents();

            int totalEvents = events.Count();
            int totalPages = (int)Math.Ceiling((double)totalEvents / pageSize);

            var paginatedEvents = events
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.EventList = events;

            return View();
        }


    }
}
