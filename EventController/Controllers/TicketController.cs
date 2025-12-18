using EventController.Models.DAO.Implements;
using EventController.Models.Entity;
using EventController.Models.ViewModels;
using EventController.Util;
using EventController.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EventController.Controllers
{
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;
        private readonly TicketDAO _ticketDAO;
        private readonly UserDAO _userDAO;
        private readonly EventDAO _eventDAO;
        private readonly IHubContext<TicketHub> _ticketHub;

        public TicketController(ILogger<TicketController> logger, TicketDAO ticketDAO, UserDAO userDAO, EventDAO eventDAO, IHubContext<TicketHub> ticketHub)
        {
            _logger = logger;
            _ticketDAO = ticketDAO;
            _userDAO = userDAO;
            _eventDAO = eventDAO;
            _ticketHub = ticketHub;
        }

        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }

            var user = _userDAO.GetUserByEmail(currentUser.Email);
            if (user == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }

            // Get all tickets for the user
            var tickets = _ticketDAO.GetUserTickets(user.UserID);

            // Group tickets by event
            var groupedTickets = tickets
                .GroupBy(t => new { t.EventID, t.Event.Title, t.Event.StartTime, t.Event.EndTime, t.Event.Venue, t.Event.ImageUrl, t.Event.Status })
                .Select(g => new
                {
                    Event = g.Key,
                    Tickets = g.ToList(),
                    TotalTickets = g.Count(),
                    UnusedTickets = g.Count(t => t.Status == "Unused"),
                    UsedTickets = g.Count(t => t.Status == "Used"),
                    CancelledTickets = g.Count(t => t.Status == "Cancelled")
                })
                .OrderByDescending(g => g.Event.StartTime)
                .ToList();

            ViewBag.GroupedTickets = groupedTickets;
            ViewBag.TotalTickets = tickets.Count;

            return View();
        }

        // Organizer: Manage tickets for their event
        public IActionResult ManageEventTickets(int eventId)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }

            var user = _userDAO.GetUserByEmail(currentUser.Email);
            if (user.RoleID != 2) // Must be organizer
            {
                TempData["Error"] = "Only organizers can manage event tickets.";
                return RedirectToAction("Index", "Home");
            }

            var evt = _eventDAO.GetEventById(eventId);
            if (evt == null || evt.OrganizerID != user.UserID)
            {
                TempData["Error"] = "Event not found or you don't have permission to manage its tickets.";
                return RedirectToAction("EventOrganizer", "Event");
            }

            var tickets = _ticketDAO.GetTicketsByEvent(eventId);
            ViewBag.Event = evt;
            ViewBag.Tickets = tickets;
            ViewBag.UnusedCount = tickets.Count(t => t.Status == "Unused");
            ViewBag.UsedCount = tickets.Count(t => t.Status == "Used");
            ViewBag.CancelledCount = tickets.Count(t => t.Status == "Cancelled");

            return View();
        }

        // Organizer: Mark ticket as used
        [HttpPost]
        public async Task<IActionResult> MarkAsUsed(int ticketId, int eventId)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                return Json(new { success = false, message = "Please sign in." });
            }

            var user = _userDAO.GetUserByEmail(currentUser.Email);
            if (user.RoleID != 2)
            {
                return Json(new { success = false, message = "Only organizers can mark tickets as used." });
            }

            var evt = _eventDAO.GetEventById(eventId);
            if (evt == null || evt.OrganizerID != user.UserID)
            {
                return Json(new { success = false, message = "You don't have permission to manage this event's tickets." });
            }

            if (evt.Status != "Active")
            {
                return Json(new { success = false, message = "Tickets can only be marked as used when event status is Active." });
            }

            var ticket = _ticketDAO.GetTicketById(ticketId);
            if (ticket == null || ticket.EventID != eventId)
            {
                return Json(new { success = false, message = "Ticket not found." });
            }

            if (ticket.Status != "Unused")
            {
                return Json(new { success = false, message = "Only unused tickets can be marked as used." });
            }

            bool result = _ticketDAO.MarkTicketAsUsed(ticketId);
            if (result)
            {
                // Broadcast ticket status update to organizers viewing this event
                await _ticketHub.Clients.Group($"organizer_{eventId}").SendAsync("TicketStatusUpdate", new
                {
                    ticketId = ticketId,
                    status = "Used",
                    timestamp = DateTime.Now
                });
                
                return Json(new { success = true, message = "Ticket marked as used successfully." });
            }

            return Json(new { success = false, message = "Failed to mark ticket as used." });
        }
    }
}
