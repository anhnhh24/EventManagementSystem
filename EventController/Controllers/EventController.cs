using EventController.Models.DAO.Implements;
using EventController.Models.ViewModels;
using EventController.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventController.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        EventCategoryDAO _categoryDAO;
        EventDAO _eventDAO;
        VenueDAO _venueDAO;
        UserDAO _userDAO;
        RegistrationDAO _registrationDAO;
        public EventController(ILogger<EventController> logger, EventDAO eventDAO, EventCategoryDAO categoryDAO, VenueDAO venueDAO, UserDAO userDAO, RegistrationDAO registrationDAO)
        {
            _logger = logger;
            _eventDAO = eventDAO;
            _categoryDAO = categoryDAO;
            _venueDAO = venueDAO;
            _userDAO = userDAO;
            _registrationDAO = registrationDAO;
        }

        public IActionResult Index(int id)
        {
            if (id != null)
            {
                var evt = _eventDAO.GetEventById(id);
                if (evt == null)
                    return RedirectToAction("Index", "Home");
                ViewBag.Event = evt;
            }
            return View();
        }
        public IActionResult EventList(
        int? categoryId,
        int? venueId,
        DateTime? startDate,
        string searchName,
        int page = 1,
        int pageSize = 8)
        {
            ViewBag.listCategory = _categoryDAO.GetAllCategories();
            ViewBag.listVenue = _venueDAO.GetAllVenues();

            if (startDate.HasValue && startDate.Value.Date < DateTime.Today)
            {
                ViewBag.Error = "Start date must be today or later.";
                ViewBag.listEvent = new List<Event>();
                ViewBag.TotalPages = 0;
                ViewBag.CurrentPage = 1;
                return View();
            }

            var query = _eventDAO.GetQueryableEvents();

            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(e => e.Title.ToLower().Contains(searchName.ToLower()));

            if (categoryId.HasValue && categoryId > 0)
                query = query.Where(e => e.CategoryID == categoryId.Value);

            if (venueId.HasValue && venueId > 0)
                query = query.Where(e => e.VenueID == venueId.Value);

            if (startDate.HasValue)
                query = query.Where(e => e.StartTime.Date == startDate.Value.Date);

            page = Math.Max(page, 1);
            pageSize = Math.Max(pageSize, 8);

            int totalEvents = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalEvents / pageSize);
            if (page > totalPages && totalPages > 0)
                page = totalPages;

            var list = query
                .OrderBy(e => e.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.listEvent = list;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            ViewBag.SelectedCat = categoryId;
            ViewBag.SelectedVenue = venueId;
            ViewBag.SelectedDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.SearchName = searchName;

            return View();
        }
        public IActionResult CreateEvent()
        {
            UserViewModel user = HttpContext.Session.GetObject<UserViewModel>("currentUser");

            if (user == null)
            {
                ViewBag.Error = "You must be logged in to create an event.";
                return RedirectToAction("SignIn", "Authentication");
            }
            if (user.RoleID != 2)
            {
                TempData["Error"] = "You can't create event";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.listCategory = _categoryDAO.GetAllCategories();
            ViewBag.listVenue = _venueDAO.GetAllVenues();
            return View();
        }
        [HttpGet]
        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            var user = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            var evt = _eventDAO.GetEventById(id);
            if (evt == null || evt.Organizer.Email != user.Email)
            {
                TempData["Error"] = "Event invalid";
                return RedirectToAction("EventOrganizer", "Event");
            }
            if (evt.Status != "Inactive")
            {
                TempData["Notification"] = "You can not update active event";
                return RedirectToAction("EventOrganizer", "Event");
            }
            var vm = new EventViewModel
            {
                Title = evt.Title,
                Description = evt.Description,
                StartTime = evt.StartTime,
                EndTime = evt.EndTime,
                CategoryID = evt.CategoryID,
                VenueID = evt.VenueID,
                MaxAttendees = evt.MaxAttendees,
                Price = evt.Price,
            };
            ViewBag.EventId = evt.EventID;
            ViewBag.ImageUrl = evt.ImageUrl;
            ViewBag.listCategory = _categoryDAO.GetAllCategories();
            ViewBag.listVenue = _venueDAO.GetAllVenues();
            return View(vm);
        }
        [HttpPost]
        public IActionResult EditEvent(EventViewModel model, string EventId)
        {
            ViewBag.listCategory = _categoryDAO.GetAllCategories();
            ViewBag.listVenue = _venueDAO.GetAllVenues();
            var user = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            var evt = _eventDAO.GetEventById(int.Parse(EventId));
            if(evt.Status != "Inactive")
            {
                ViewBag.Notification = "You can not update active event";
                return View();
            }
            if (ModelState.IsValid)
            {
               
                if (evt == null || evt.Organizer.Email != user.Email)
                {
                    ViewBag.Notification = "Event invalid";
                    return RedirectToAction("EventOrganizer", "Event");
                }

                if (model.StartTime >= model.EndTime)
                {
                    ModelState.AddModelError("EndTime", "End time must be after start time.");
                }

                if (model.VenueID.HasValue)
                {
                    bool isOccupied = _eventDAO.IsVenueOccupied(model.VenueID.Value, model.StartTime, model.EndTime, int.Parse(EventId));
                    if (isOccupied)
                    {
                        ModelState.AddModelError("VenueID", "There is already another event scheduled at this venue during the selected time.");
                    }

                    var venue = _venueDAO.GetVenueById(model.VenueID.Value);
                    if (venue != null && model.MaxAttendees.HasValue && model.MaxAttendees.Value > venue.Capacity)
                    {
                        ModelState.AddModelError("MaxAttendees", $"Max attendees exceed venue capacity of {venue.Capacity}.");
                    }
                }

                string fileName = null;

                if (model.EventImage != null && model.EventImage.Length > 0 && !string.IsNullOrEmpty(evt.ImageUrl) )
                {

                    ImageService imgService = new ImageService();
                    fileName = imgService.SaveImage(model.EventImage, "events");
                }

                evt.Title = model.Title;
                evt.Description = model.Description;
                evt.StartTime = model.StartTime;
                evt.EndTime = model.EndTime;
                evt.VenueID = model.VenueID;
                evt.CategoryID = model.CategoryID;
                evt.MaxAttendees = model.MaxAttendees;
                evt.Price = model.Price;
                if (!string.IsNullOrEmpty(fileName))
                {
                    evt.ImageUrl = fileName;
                }

                _eventDAO.UpdateEvent(evt);
                ViewBag.Notification = "Event update successfully";
                ViewBag.EventId = evt.EventID;
                ViewBag.ImageUrl = evt.ImageUrl;
                return View();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateEvent(EventViewModel model)
        {
            var user = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (user == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }

            if (model.StartTime >= model.EndTime)
            {
                ModelState.AddModelError("EndTime", "End time must be after start time.");
            }

            if (model.VenueID.HasValue)
            {
                bool isOccupied = _eventDAO.IsVenueOccupied(model.VenueID.Value, model.StartTime, model.EndTime, -1);
                if (isOccupied)
                {
                    ModelState.AddModelError("VenueID", "There is already another event scheduled at this venue during the selected time.");
                }

                var venue = _venueDAO.GetVenueById(model.VenueID.Value);
                if (venue != null && model.MaxAttendees.HasValue && model.MaxAttendees.Value > venue.Capacity)
                {
                    ModelState.AddModelError("MaxAttendees", $"Max attendees exceed venue capacity of {venue.Capacity}.");
                }
            }
            model.Status = "Inactive";
            if(model.Price == null)
            {
                model.Price = 0;
            }    
            if (!ModelState.IsValid)
            {
                ViewBag.listCategory = _categoryDAO.GetAllCategories();
                ViewBag.listVenue = _venueDAO.GetAllVenues();
                return View(model);
            }

            string fileName = null;

            if (model.EventImage != null && model.EventImage.Length > 0)
            {

                ImageService imgService = new ImageService();
                fileName = imgService.SaveImage(model.EventImage, "events");
            }

            var evt = new Event
            {
                Title = model.Title,
                Description = model.Description,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                VenueID = model.VenueID,
                CategoryID = model.CategoryID,
                MaxAttendees = model.MaxAttendees,
                Price = model.Price,
                Status = model.Status,
                OrganizerID = _userDAO.GetUserByEmail(user.Email).UserID,
                CreatedAt = DateTime.Now,
                CurrentAttendees = 0,
                ImageUrl = fileName
            };

            _eventDAO.AddEvent(evt);
            TempData["Notification"] = "Event created successfully. Waiting for admin approval.";
            return RedirectToAction("EventOrganizer", "Event");
        }

        public IActionResult EventOrganizer(string sortBy)
        {
            var user = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (user == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }
            if(user.RoleID != 2)
            {
                TempData["Error"] = "You can't access this page";
                return RedirectToAction("Index", "Home");
            }
            var User = _userDAO.GetUserByEmail(user.Email);
            var events = _eventDAO.GetEventsByOrganizer(User.UserID);
            switch (sortBy)
            {
                case "attenddees":
                    events = events.OrderBy(e => e.MaxAttendees).ToList();
                    break;
                case "price":
                    events = events.OrderByDescending(e => e.Price).ToList();
                    break;
                case "status":
                    events = events.OrderBy(e => e.Status).ToList();
                    break;
                case "start":
                    events = events.OrderBy(e => e.StartTime).ToList();
                    break;
                default:
                    break;

            }
            ViewBag.EventList = events;
            return View();
        }

        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                TempData["Error"] = "Please login to delete events.";
                return RedirectToAction("SignIn", "Authentication");
            }

            var evt = _eventDAO.GetEventById(id);
            if (evt == null)
            {
                TempData["Error"] = "Event not found.";
                return RedirectToAction("EventOrganizer", "Event");
            }

            // Get current user ID
            var user = _userDAO.GetUserByEmail(currentUser.Email);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("SignIn", "Authentication");
            }

            // Check ownership - only allow organizer to delete their own events
            if (evt.OrganizerID != user.UserID)
            {
                TempData["Error"] = "You can only delete your own events.";
                return RedirectToAction("EventOrganizer", "Event");
            }

            // Only allow deletion of Inactive events (not yet approved by admin)
            if (evt.Status != "Inactive")
            {
                TempData["Error"] = "You can only delete inactive events. Active, upcoming, or ongoing events cannot be deleted.";
                return RedirectToAction("EventOrganizer", "Event");
            }

            // Check if event has any registrations
            if (evt.Registrations != null && evt.Registrations.Any())
            {
                TempData["Error"] = $"Cannot delete event '{evt.Title}' because it has {evt.Registrations.Count} registration(s). Please cancel the event instead.";
                return RedirectToAction("EventOrganizer", "Event");
            }

            _eventDAO.DeleteEvent(id);
            TempData["Notification"] = "Event deleted successfully.";
            return RedirectToAction("EventOrganizer", "Event");
        }
    }


}