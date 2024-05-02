using Microsoft.AspNetCore.Mvc;

namespace OsiteNew.Controllers {
    public class HomeController : Controller {
        public IActionResult HomePage() {

            return View();
        }
    }
}
