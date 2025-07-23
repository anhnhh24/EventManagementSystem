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
        EmailVerificationTokenDAO _emailDAO;

        public AdminController( UserDAO userDAO, EmailVerificationTokenDAO emailDAO, EventDAO eventDAO)
        {
            _userDAO = userDAO;
            _emailDAO = emailDAO;
            _eventDAO = eventDAO;
        }
        public IActionResult UserAdmin()
        {
            var users = _userDAO.GetAllUsers();
            ViewBag.listUser = users;
            return View();
        }

        public IActionResult EventAdmin(string sortBy)
        {
            var events = _eventDAO.GetAllEvents();
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
    }
}
