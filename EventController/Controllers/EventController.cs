using Microsoft.AspNetCore.Mvc;

namespace EventController.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EventList()
        {
            return View();
        }
    }
}
