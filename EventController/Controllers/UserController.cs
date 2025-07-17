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

            if (model.ProfileImage != null && model.ProfileImage.Length > 0)
            {
                var fileName = Path.GetFileName(model.ProfileImageFile.FileName);

                var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "avatars");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var filePath = Path.Combine(uploadFolder, uniqueFileName); 

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImageFile.CopyToAsync(stream); 
                }

                user.ProfileImage = $"/uploads/avatars/{uniqueFileName}";
            }

            _userDAO.UpdateUser(user);
            return RedirectToAction("Index", "Home");
        }


    }
}
