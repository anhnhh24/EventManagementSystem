using EventController.Models.DAO.Implements;
using EventController.Models.DTO;
using EventController.Models.Entity;
using EventController.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventController.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        UserDAO _userDAO;
        EmailVerificationTokenDAO _emailDAO;

        public AuthenticationController(ILogger<AuthenticationController> logger, UserDAO userDAO, EmailVerificationTokenDAO emailDAO)
        {
            _logger = logger;
            _userDAO = userDAO;
            _emailDAO = emailDAO;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignIn(UserDTO user)
        {
            if (!string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.Password))
            {
                var existingUser = _userDAO.GetUserByEmail(user.Email);

                if (existingUser == null || !_userDAO.VerifyPassword(existingUser, user.Password))
                {
                    ViewBag.Error = "Email or password invalid";
                    return View();
                }
                else
                {
                    UserDTO SessionUser = new UserDTO
                    {
                        FullName = existingUser.FullName,
                        Email = existingUser.Email,
                        Address = existingUser.Address,
                        DoB = existingUser.DoB,
                        Phone = existingUser.Phone,
                        RoleID = existingUser.RoleID,
                        ProfileImage = existingUser.ProfileImage
                    };
                    HttpContext.Session.SetObject("currentUser", SessionUser);
                    
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO newUser)
        {
            if (ModelState.IsValid)
            {

                if (_userDAO.UserExistsByEmail(newUser.Email))
                {
                    ModelState.AddModelError(nameof(newUser.Email), "This e-mail is already registered.");
                }

                if (newUser.DoB != null)
                {
                    var today = DateTime.Today;
                    int age = today.Year - newUser.DoB.Year;
                    if (newUser.DoB.Date > today.AddYears(-age)) age--;

                    if (age < 18)
                        ModelState.AddModelError(nameof(newUser.DoB), "You must be at least 18 years old.");
                }

                if (!ModelState.IsValid)
                {
                    return View("SignUp");        
                }

                var user = new User
                {
                    FullName = newUser.FullName,
                    Email = newUser.Email,
                    Password = PasswordHelper.HashPassword(newUser.Password),
                    RoleID = newUser.RoleID,
                    Phone = newUser.Phone,
                    Gender = newUser.Gender,
                    DoB = newUser.DoB,
                    Address = newUser.Address,
                    Status = "Active",
                    IsEmailVerified = false
                };
                _userDAO.AddUser(user);

                User user1 = _userDAO.GetUserByEmail(user.Email);

                string verifyToken = _emailDAO.GenerateTokenAsync(user1.UserID).Result.Token;

                var confirmationLink = Url.Action("ConfirmEmail", "Authentication", new { userId = user1.UserID, token = verifyToken }, Request.Scheme);

                var emailService = new EmailService();
                string subject = "Email Confirmation";
                string content = $@"
                                <h2>Hi {newUser.FullName},</h2>
                                <p>Thank you for registering. Please confirm your email by clicking the link below:</p>
                                <p><a href='{confirmationLink}'>Confirm Email</a></p>";

                await emailService.SendConfirmationEmailAsync(newUser.Email, newUser.FullName, subject, content);
                ViewBag.Notification = "Registration Successful, please verify account by your sign up email";
                return View("SignIn");
            }

            return View("SignUp");
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try
            {
                int Id = int.Parse(userId);
                User user = _userDAO.GetUserById(Id);
                if (user != null)
                {
                    if (!user.IsEmailVerified)
                    {
                        EmailVerificationToken verifyToken = _emailDAO.GetValidTokenAsync(token, Id).Result;
                        if (verifyToken.ExpiresAt > DateTime.Now)
                        {
                            if (verifyToken.Token.Equals(token))
                            {
                                user.IsEmailVerified = true;
                                _userDAO.UpdateUser(user);
                                ViewBag.Notification = "Email confirmed";
                                return View("SignIn");
                            }
                            else
                            {
                                ViewBag.Notification = "Can not veritfy user's email";
                                return View("SignIn");
                            }
                        }
                        else
                        {
                            ViewBag.Notification = "Your confirmation link has expired";
                            return View("SignIn");
                        }
                    }
                    else
                    {
                        ViewBag.Notification = "Your account is verified";
                        return View("SignIn");
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Notification = "An error occur when verify user email";
                return View("SignIn");
            }
            return View("SignIn");
        }
    }
}
