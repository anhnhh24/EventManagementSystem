
using EventController.Models.DAO.Implements;
using EventController.Models.ViewModels;
using EventController.Util;
using Microsoft.AspNetCore.Mvc;


namespace EventController.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    EventCategoryDAO _categoryDAO;
    UserDAO _userDAO;
    EventDAO _eventDAO;
    VenueDAO _venueDAO;
    NotificationDAO _notificationDAO;
    public List<EventCategory> listCategory { get; set; }
    public List<Event> listEvent { get; set; }
    public List<Venue> listVenue { get; set; }
    public HomeController(ILogger<HomeController> logger, EventCategoryDAO categoryDAO, UserDAO userDAO, EventDAO eventDAO, VenueDAO venueDAO, NotificationDAO notificationDAO)
    {
        _logger = logger;
        _categoryDAO = categoryDAO;
        _userDAO = userDAO;
        _eventDAO = eventDAO;
        _venueDAO = venueDAO;
        _notificationDAO = notificationDAO;
    }

    public IActionResult Index()
    {
        UserViewModel user = HttpContext.Session.GetObject<UserViewModel>("currentUser");

        if (user != null)
        {
            var currentUser = _userDAO.GetUserByEmail(user.Email);
            var listNotification = _notificationDAO.GetUserNotification(currentUser.UserID);
            if (listNotification != null && listNotification.Count > 0)
            {
                ViewBag.listNotification = listNotification;
                foreach (var notification in listNotification)
                {
                    _notificationDAO.MarkAsSent(notification);
                }
            }
            else
            {
                ViewBag.listNotification = null;
            }
        }
        listCategory = _categoryDAO.GetAllCategories();
        listEvent = _eventDAO.GetUpcomingEvents();
        listVenue = _venueDAO.GetAllVenues();
        ViewBag.listExpiredEvent = _eventDAO.GetAllExpiredEvent();
        ViewBag.listEventIn1Month = _eventDAO.GetAllEventsThisMonth();
        ViewBag.listCategory = listCategory;
        ViewBag.listVenue = listVenue;
        ViewBag.listEvent = listEvent;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

}
