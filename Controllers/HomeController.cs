using Microsoft.AspNetCore.Mvc;

namespace AccountKeep.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // ViewData used to pass data to the view (page title, CSS class)
            ViewData["Title"] = "Home";
            ViewData["BodyClass"] = "home-bg";
            return View();
        }

        public IActionResult About()
        {
            // ViewData used to pass data to the view (page title, CSS class)
            ViewData["Title"] = "About";
            ViewData["BodyClass"] = "about-bg";
            return View();
        }

        public IActionResult Contact()
        {
            // ViewData used to pass data to the view (page title, CSS class)
            ViewData["Title"] = "Contact";
            ViewData["BodyClass"] = "contact-bg";
            return View();
        }


    }
}
