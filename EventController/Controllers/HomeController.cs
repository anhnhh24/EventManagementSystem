
using EventController.Models.DAO.Implements;
using Microsoft.AspNetCore.Mvc;


namespace EventController.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    CategoryDAO _categoryDAO;
    UserDAO _userDAO;
    EventDAO _eventDAO;
    VenueDAO _venueDAO;
    public List<Category> listCategory { get; set; }
    public List<Event> listEvent { get; set; }
    public List<Venue> listVenue { get; set; }
    public HomeController(ILogger<HomeController> logger, CategoryDAO categoryDAO, UserDAO userDAO, EventDAO eventDAO, VenueDAO venueDAO)
    {
        _logger = logger;
        _categoryDAO = categoryDAO;
        _userDAO = userDAO;
        _eventDAO = eventDAO;
        _venueDAO = venueDAO;
    }

    public IActionResult Index()
    {
        listCategory = _categoryDAO.GetAllCategories();
        listEvent = _eventDAO.GetAllEvents();
        listVenue = _venueDAO.GetAllVenues();
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
