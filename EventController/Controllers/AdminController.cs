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

        public AdminController( UserDAO userDAO, EmailVerificationTokenDAO emailDAO, EventDAO eventDAO, EventCategoryDAO eventCategory)
        {
            _userDAO = userDAO;
            _emailDAO = emailDAO;
            _eventCategoryDAO = eventCategory;
            _eventDAO = eventDAO;
        }
        public IActionResult UserAdmin()
        {
            var users = _userDAO.GetAllUsers();
            ViewBag.listUser = users;
            return View();
        }

        public IActionResult EventAdmin(string status, int? category)
        {
            var events = _eventDAO.GetAllEvents();

            if (category.HasValue)
            {
                events = events.Where(e => e.CategoryID == category.Value).ToList();
            }

            if(!string.IsNullOrEmpty(status))
            {
                events = events.Where(e => e.Status == status).ToList();
            }

            ViewBag.listCategory = _eventCategoryDAO.GetAllCategories();
            ViewBag.EventList = events;
            return View();
        }

        public IActionResult AcceptEvent(int id)
        {
            var events = _eventDAO.GetEventById(id);

            events.Status = "Upcoming";

            _eventDAO.UpdateEvent(events);
            return RedirectToAction("EventAdmin", "Admin");
        }

    }
}
