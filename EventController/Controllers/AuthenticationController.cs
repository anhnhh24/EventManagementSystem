using EventController.Models.DAO.Implements;
using EventController.Models.Entity;
using EventController.Models.ViewModels;
using EventController.Util;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult SignIn(UserViewModel user)
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
                    UserViewModel SessionUser = new UserViewModel
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
                    if (existingUser.RoleID == 1)
                    {
                        return RedirectToAction("UserAdmin", "Admin");

                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel newUser)
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
                                _emailDAO.MarkTokenAsUsedAsync(verifyToken).Wait();
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
            catch (Exception ex)
            {
                ViewBag.Notification = "An error occur when verify user email";
                return View("SignIn");
            }
            return View("SignIn");
        }

        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email is required.");
                return View();
            }

            var user = _userDAO.GetUserByEmail(email);
            if (user == null)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            var token = _emailDAO.GenerateTokenAsync(user.UserID);
            var resetLink = Url.Action("ResetPassword", "Authentication", new { token = token.Result.Token, userID = user.UserID }, Request.Scheme);

            var emailService = new EmailService();
            string subject = "Forgot Password";
            string content = $@"
                                <h2>Hi {user.FullName},</h2>
                                <p>We received a request to reset your password.</p>
                                <p>Please click the link below to set a new password:</p>
                                <p><a href='{resetLink}'>Reset Password</a></p>
                                <p>If you did not request this, please ignore this email.</p>
                                <p style='font-size: 12px; color: gray;'>This link will expire in 30 minutes.</p>";

            await emailService.SendConfirmationEmailAsync(email, user.FullName, subject, content);

            return View("SignIn");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            try
            {
                int id = int.Parse(userId);
                User user = _userDAO.GetUserById(id);
                if (user != null)
                {
                    EmailVerificationToken resetToken = _emailDAO.GetValidTokenAsync(token, id).Result;
                    if (resetToken != null && resetToken.ExpiresAt > DateTime.Now && !resetToken.IsUsed)
                    {
                        ViewBag.UserId = userId;
                        ViewBag.Token = token;
                        return View();
                    }
                    else
                    {
                        ViewBag.Notification = "Your password reset link is invalid or expired.";
                        return View("SignIn");
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Notification = "An error occurred while verifying your reset link.";
                return View("SignIn");
            }
            ViewBag.Notification = "Invalid reset request.";
            return View("SignIn");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string userId, string token, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword != confirmPassword)
                {
                    ViewBag.Error = "Passwords do not match.";
                    ViewBag.UserId = userId;
                    ViewBag.Token = token;
                    return View();
                }

                int id = int.Parse(userId);
                User user = _userDAO.GetUserById(id);
                if (user != null)
                {
                    EmailVerificationToken resetToken = _emailDAO.GetValidTokenAsync(token, id).Result;
                    if (resetToken != null && resetToken.ExpiresAt > DateTime.Now && !resetToken.IsUsed)
                    {
                        user.Password = PasswordHelper.HashPassword(newPassword);
                        _userDAO.UpdateUser(user);
                        _emailDAO.MarkTokenAsUsedAsync(resetToken).Wait();

                        ViewBag.Notification = "Your password has been reset successfully.";
                        return View("SignIn");
                    }
                }
                ViewBag.Error = "Invalid or expired token.";
                return View("SignIn");
            }
            catch
            {
                ViewBag.Error = "An error occurred while resetting the password.";
                return View("SignIn");
            }
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }
            return View();

        }

        [HttpPost]
        public IActionResult ChangePassword(string newPassword, string confirmPassword, string oldPassword)
        {
            var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            var user = _userDAO.GetUserByEmail(currentUser.Email);
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                ViewBag.Error = "Please fill in both password fields.";
                return View();
            }

            if (!PasswordHelper.VerifyPassword(user.Password, oldPassword))
            {
                ViewBag.Error = "Wrong Password";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }


            user.Password = PasswordHelper.HashPassword(newPassword);
            _userDAO.UpdateUser(user);

            TempData["Notification"] = "Your password has been changed successfully. Please sign in again.";
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "Authentication");
        }


    }

}
