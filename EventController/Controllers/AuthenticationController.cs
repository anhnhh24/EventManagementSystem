using Microsoft.AspNetCore.Mvc;

namespace EventController.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
