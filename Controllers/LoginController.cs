using AccountKeep.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountKeep.Controllers
{
    public class LoginController : Controller
    {
        // Dependency-injected service for user authentication
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            // Set page info for view
            ViewData["Title"] = "Log In";
            ViewData["BodyClass"] = "login-bg";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // Stores username in session to track login
            if (_userService.Authenticate(username, password))
            {
                // Save simple session
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Persons");
            }

            // Pass error message to view if login fails
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult Logout()
        {
            // Clear session on logout
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
