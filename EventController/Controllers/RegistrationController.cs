using EventController.Models.DAO.Implements;
using EventController.Models.Entity;
using EventController.Models.ViewModels;
using EventController.Util;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace EventController.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ILogger<RegistrationController> _logger;

        RegistrationDAO _registrationDAO;
        EventCategoryDAO _categoryDAO;
        EventDAO _eventDAO;
        VenueDAO _venueDAO;
        UserDAO _userDAO;

        public RegistrationController(ILogger<RegistrationController> logger, EventDAO eventDAO, EventCategoryDAO categoryDAO, VenueDAO venueDAO, UserDAO userDAO, RegistrationDAO registrationDAO)
        {
            _logger = logger;
            _eventDAO = eventDAO;
            _categoryDAO = categoryDAO;
            _venueDAO = venueDAO;
            _userDAO = userDAO;
            _registrationDAO = registrationDAO;
        }

        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }
            var user = _userDAO.GetUserByEmail(currentUser.Email);
            if (user.RoleID != 3)
            {
                TempData["Error"] = "You can't";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.listRegistration = _registrationDAO.getPendingUserRegistration(user.UserID);
            return View();
        }
        public IActionResult Remove(int Id)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }
            var user = _userDAO.GetUserByEmail(currentUser.Email);
            if (user.RoleID != 3)
            {
                TempData["Error"] = "You can't";
                return RedirectToAction("Index", "Home");
            }
            bool success =  _registrationDAO.CancelRegistration(Id);
            ViewBag.listRegistration = _registrationDAO.getPendingUserRegistration(user.UserID);
            return View("Index");
        }
        public IActionResult UpdateQuantity(int id, string actionType)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }
            var user = _userDAO.GetUserByEmail(currentUser.Email);
            var reg = _registrationDAO.GetById(id);
            if (reg == null) return  View("Index");
            if(reg.Quantity == 1 && actionType == "Decrease")
            {
                _registrationDAO.CancelRegistration(id);
                return View("Index");
            }
            if (actionType == "Increase")
            {
                reg.Quantity += 1;
                if(!_registrationDAO.IsValidEventAttendees(reg.EventID, 1))
                {
                    return View("Index");
                }
            }
            else if (actionType == "Decrease" && reg.Quantity > 1)
            {
                reg.Quantity -= 1;

            }
            reg.Total = reg.Quantity * reg.Event.Price;
            _registrationDAO.Update(reg);
            ViewBag.listRegistration = _registrationDAO.getPendingUserRegistration(user.UserID);
            return View("Index");
        }



        [HttpPost]
        public IActionResult Register(int eventId)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }
            var user = _userDAO.GetUserByEmail(currentUser.Email);
            if (user.RoleID != 3)
            {
                TempData["Error"] = "You can register this event";
                return RedirectToAction("Index", "Home");
            }    
            List<Registration> registrations = _registrationDAO.getPendingUserRegistration(user.UserID);
            var registration = registrations.FirstOrDefault(r => r.EventID == eventId);
            bool success = false;

            if (registration != null)
            {
                registration.Quantity += 1;
                _registrationDAO.Update(registration);
            }
            else
            {
                success = _registrationDAO.RegisterUserToEvent(user.UserID, eventId);
            }

            if (success)
            {
                TempData["Success"] = "Registration successful!";
            }


            return RedirectToAction("Index", "Home");
        }



    }
}
