using EventController.Models.DAO.Implements;
using EventController.Models.ViewModels;
using EventController.Util;
using Microsoft.AspNetCore.Mvc;

namespace EventController.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        EventCategoryDAO _categoryDAO;
        UserDAO _userDAO;
        EventDAO _eventDAO;
        VenueDAO _venueDAO;
        public UserController(ILogger<UserController> logger, EventCategoryDAO categoryDAO, UserDAO userDAO, EventDAO eventDAO, VenueDAO venueDAO)
        {
            _logger = logger;
            _categoryDAO = categoryDAO;
            _userDAO = userDAO;
            _eventDAO = eventDAO;
            _venueDAO = venueDAO;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditUserProfile()
        {
            var sessionUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (sessionUser == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }

            var user = _userDAO.GetUserByEmail(sessionUser.Email);
            if (user == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }

            EditUserViewModel vm = new EditUserViewModel
            {
                UserID = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                DoB = user.DoB,
                Address = user.Address,
                ProfileImage = user.ProfileImage
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult EditUserProfile(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userDAO.GetUserById(model.UserID);
            if (user == null) return NotFound();

            user.FullName = model.FullName;
            user.Phone = model.Phone;
            user.Gender = model.Gender;
            user.DoB = model.DoB;
            user.Address = model.Address;

            if (model.ProfileImageFile != null && model.ProfileImageFile.Length > 0)
            {
                ImageService imageService = new ImageService();
                user.ProfileImage = imageService.SaveImage(model.ProfileImageFile, "avartars");
            }

            EditUserViewModel vm = new EditUserViewModel
            {
                UserID = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                DoB = user.DoB,
                Address = user.Address,
                ProfileImage = user.ProfileImage
            };
            HttpContext.Session.Clear();
            UserViewModel SessionUser = new UserViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                DoB = user.DoB,
                Phone = user.Phone,
                RoleID = user.RoleID,
                ProfileImage = user.ProfileImage
            };
            HttpContext.Session.SetObject("currentUser", SessionUser);
            _userDAO.UpdateUser(user);
            return View(vm);
        }


    }
}
