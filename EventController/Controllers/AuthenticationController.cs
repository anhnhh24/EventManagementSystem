using EventController.Models.DAO.Implements;
using EventController.Models.DTO;
using EventController.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EventController.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        UserDAO _userDAO;

        public AuthenticationController(ILogger<AuthenticationController> logger, UserDAO userDAO)
        {
            _logger = logger;
            _userDAO = userDAO;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO newUser)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = newUser.FullName,
                    Email = newUser.Email,
                    Password = newUser.Password,
                    RoleID = newUser.RoleID,
                    Phone = newUser.Phone,
                    Gender = newUser.Gender,
                    DoB = newUser.DoB,
                    Address = newUser.Address,
                    Status = "Active",
                    IsEmailVerified = false
                };

                _userDAO.AddUser(user);

                var token = Guid.NewGuid().ToString();
                var confirmationLink = Url.Action("ConfirmEmail", "Authentication", new { email = newUser.Email, token = token }, Request.Scheme);

                var emailService = new EmailService();
                string subject = "Email Confirmation";
                string content = $@"
                                <h2>Hi {newUser.FullName},</h2>
                                <p>Thank you for registering. Please confirm your email by clicking the link below:</p>
                                <p><a href='{confirmationLink}'>Confirm Email</a></p>";

                await emailService.SendConfirmationEmailAsync(newUser.Email, newUser.FullName, subject, content);
                ViewBag.Notification = "Registration Successful";
                return View("SignUp");
            }

            return View("SignUp");
        }


        public IActionResult ConfirmEmail(string email, string token)
        {
            var user = _userDAO.GetUserByEmail(email);
            if (user == null) return NotFound();

            user.IsEmailVerified = true;
            _userDAO.UpdateUser(user);
            ViewBag.Notification = "Email confirmed";
            return View("SignUp");
        }
    }
}
