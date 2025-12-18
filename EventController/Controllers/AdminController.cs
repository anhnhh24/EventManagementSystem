using EventController.Models.DAO.Implements;
using EventController.Models.ViewModels;
using EventController.Util;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EventController.Controllers
{
    public class AdminController : Controller
    {
        UserDAO _userDAO;
        EventDAO _eventDAO;
        EventCategoryDAO _eventCategoryDAO;
        EmailVerificationTokenDAO _emailDAO;
        VenueDAO _venueDAO;
        TicketDAO _ticketDAO;

        public AdminController( UserDAO userDAO, EmailVerificationTokenDAO emailDAO, EventDAO eventDAO, EventCategoryDAO eventCategory, VenueDAO venueDAO, TicketDAO ticketDAO)
        {
            _userDAO = userDAO;
            _emailDAO = emailDAO;
            _eventCategoryDAO = eventCategory;
            _eventDAO = eventDAO;
            _venueDAO = venueDAO;
            _ticketDAO = ticketDAO;
        }
        
        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to access admin panel.";
                return RedirectToAction("Index", "Home");
            }
            
            // Dashboard overview
            ViewBag.TotalUsers = _userDAO.GetAllUsers().Count;
            ViewBag.TotalEvents = _eventDAO.GetAllEvents().Count;
            ViewBag.ActiveEvents = _eventDAO.GetAllEvents().Count(e => e.Status == "Active");
            ViewBag.PendingEvents = _eventDAO.GetAllEvents().Count(e => e.Status == "Inactive");
            ViewBag.UpcomingEvents = _eventDAO.GetAllEvents().Count(e => e.Status == "Upcoming");
            ViewBag.CancelledEvents = _eventDAO.GetAllEvents().Count(e => e.Status == "Cancelled");
            return View();
        }

        public IActionResult UserAdmin()
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to access admin panel.";
                return RedirectToAction("Index", "Home");
            }
            
            var users = _userDAO.GetAllUsers();
            ViewBag.listUser = users;
            return View();
        }

        public IActionResult EventAdmin(string status, int? category, string searchTitle)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to access admin panel.";
                return RedirectToAction("Index", "Home");
            }
            
            var events = _eventDAO.GetAllEvents();

            if (category.HasValue)
            {
                events = events.Where(e => e.CategoryID == category.Value).ToList();
            }

            if(!string.IsNullOrEmpty(status))
            {
                events = events.Where(e => e.Status == status).ToList();
            }

            if (!string.IsNullOrEmpty(searchTitle))
            {
                events = events.Where(e => e.Title.ToLower().Contains(searchTitle.ToLower())).ToList();
            }

            ViewBag.listCategory = _eventCategoryDAO.GetAllCategories();
            ViewBag.EventList = events;
            ViewBag.SearchTitle = searchTitle;
            return View();
        }

        public IActionResult AcceptEvent(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to perform this action.";
                return RedirectToAction("Index", "Home");
            }
            
            var events = _eventDAO.GetEventById(id);

            events.Status = "Upcoming";

            _eventDAO.UpdateEvent(events);
            return RedirectToAction("EventAdmin", "Admin");
        }

        public IActionResult ActivateUser(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to perform this action.";
                return RedirectToAction("Index", "Home");
            }

            var user = _userDAO.GetUserById(id);
            if (user != null)
            {
                user.Status = "Active";
                _userDAO.UpdateUser(user);
                TempData["Notification"] = $"User {user.FullName} has been activated.";
            }
            else
            {
                TempData["Error"] = "User not found.";
            }

            return RedirectToAction("UserAdmin", "Admin");
        }

        public IActionResult DeactivateUser(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to perform this action.";
                return RedirectToAction("Index", "Home");
            }

            var user = _userDAO.GetUserById(id);
            if (user != null)
            {
                if (user.RoleID == 1)
                {
                    TempData["Error"] = "Cannot deactivate admin users.";
                    return RedirectToAction("UserAdmin", "Admin");
                }

                user.Status = "Inactive";
                _userDAO.UpdateUser(user);
                TempData["Notification"] = $"User {user.FullName} has been deactivated.";
            }
            else
            {
                TempData["Error"] = "User not found.";
            }

            return RedirectToAction("UserAdmin", "Admin");
        }

        public IActionResult CancelEvent(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to perform this action.";
                return RedirectToAction("Index", "Home");
            }

            var evt = _eventDAO.GetEventById(id);
            if (evt != null)
            {
                if (evt.Status == "Expired")
                {
                    TempData["Error"] = "Cannot cancel an expired event.";
                    return RedirectToAction("EventAdmin", "Admin");
                }

                evt.Status = "Cancelled";
                _eventDAO.UpdateEvent(evt);

                // Cancel all unused tickets for this event
                _ticketDAO.CancelAllEventTickets(id);

                TempData["Notification"] = $"Event '{evt.Title}' has been cancelled and all unused tickets have been cancelled.";
            }
            else
            {
                TempData["Error"] = "Event not found.";
            }

            return RedirectToAction("EventAdmin", "Admin");
        }

        public IActionResult VenueAdmin()
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to access admin panel.";
                return RedirectToAction("Index", "Home");
            }

            var venues = _venueDAO.GetAllVenues();
            return View(venues);
        }

        [HttpGet]
        public IActionResult CreateVenue()
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to access admin panel.";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult CreateVenue(Venue venue, IFormFile? imageFile)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to access admin panel.";
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrWhiteSpace(venue.Name))
            {
                ModelState.AddModelError("Name", "Venue name is required.");
            }

            if (string.IsNullOrWhiteSpace(venue.Address))
            {
                ModelState.AddModelError("Address", "Address is required.");
            }

            if (venue.Capacity.HasValue && venue.Capacity <= 0)
            {
                ModelState.AddModelError("Capacity", "Capacity must be greater than 0.");
            }

            if (!ModelState.IsValid)
            {
                return View(venue);
            }

            try
            {
                // Handle image upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "venues");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    venue.Image = "/img/venues/" + uniqueFileName;
                }
                else
                {
                    venue.Image = "/img/venues/default-venue.png";
                }

                _venueDAO.AddVenue(venue);
                TempData["Notification"] = $"Venue '{venue.Name}' has been created successfully.";
                return RedirectToAction("VenueAdmin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the venue: " + ex.Message);
                return View(venue);
            }
        }

        [HttpGet]
        public IActionResult EditVenue(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to access admin panel.";
                return RedirectToAction("Index", "Home");
            }

            var venue = _venueDAO.GetVenueById(id);
            if (venue == null)
            {
                TempData["Error"] = "Venue not found.";
                return RedirectToAction("VenueAdmin");
            }

            return View(venue);
        }

        [HttpPost]
        public IActionResult EditVenue(Venue venue, IFormFile? imageFile)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to access admin panel.";
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrWhiteSpace(venue.Name))
            {
                ModelState.AddModelError("Name", "Venue name is required.");
            }

            if (string.IsNullOrWhiteSpace(venue.Address))
            {
                ModelState.AddModelError("Address", "Address is required.");
            }

            if (venue.Capacity.HasValue && venue.Capacity <= 0)
            {
                ModelState.AddModelError("Capacity", "Capacity must be greater than 0.");
            }

            if (!ModelState.IsValid)
            {
                return View(venue);
            }

            try
            {
                var existingVenue = _venueDAO.GetVenueById(venue.VenueID);
                if (existingVenue == null)
                {
                    TempData["Error"] = "Venue not found.";
                    return RedirectToAction("VenueAdmin");
                }

                // Handle image upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "venues");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    venue.Image = "/img/venues/" + uniqueFileName;
                }
                else
                {
                    venue.Image = existingVenue.Image;
                }

                _venueDAO.UpdateVenue(venue);
                TempData["Notification"] = $"Venue '{venue.Name}' has been updated successfully.";
                return RedirectToAction("VenueAdmin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the venue: " + ex.Message);
                return View(venue);
            }
        }

        [HttpPost]
        public IActionResult DeleteVenue(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to access admin panel.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (currentUser.RoleID != 1)
            {
                TempData["Error"] = "You don't have permission to perform this action.";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var venue = _venueDAO.GetVenueById(id);
                if (venue == null)
                {
                    TempData["Error"] = "Venue not found.";
                    return RedirectToAction("VenueAdmin");
                }

                // Check if venue has any events
                if (venue.Events != null && venue.Events.Any())
                {
                    TempData["Error"] = $"Cannot delete venue '{venue.Name}' because it has {venue.Events.Count} event(s) associated with it.";
                    return RedirectToAction("VenueAdmin");
                }

                _venueDAO.DeleteVenue(id);
                TempData["Notification"] = $"Venue '{venue.Name}' has been deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the venue: " + ex.Message;
            }

            return RedirectToAction("VenueAdmin");
        }

    }
}
