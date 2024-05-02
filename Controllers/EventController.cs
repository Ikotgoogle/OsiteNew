using Microsoft.AspNetCore.Mvc;

namespace OsiteNew.Controllers {
    public class EventController : Controller {
        public IActionResult EventPage() {
            return View();
        }
    }
}
